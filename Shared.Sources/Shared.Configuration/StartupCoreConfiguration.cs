using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Extensions;
using Shared.Interfaces.Services;
using Shared.Libraries;
using Shared.Middleware;
using Shared.Models.Annotation;
using Shared.Models.Configurations;
using Shared.Models.Security;
using Shared.Models.Service.Modules;
using Shared.Security;
using Shared.Services;
using Shared.Services.Factory;
using Shared.Models.Service;

namespace Shared.Configuration;

public partial class StartupCoreConfiguration
{
    public StartupCoreConfiguration(IConfiguration configuration, ModuleNames module)
    {
        this.Configuration = configuration;
        this.Module = module;
        this.Annotation = module.getAnnotation<ModuleAnnotation>();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        if (this.Annotation is null)
            throw new ApplicationException("DON'T HAVE ANNOTATED THE CONFIGURATION FILE");

        services.AddControllers();
        services.AddAuthentication();
        services.AddAuthorization();
        services.AddHttpContextAccessor();
        

        // SECURITY CHANEL CONFIGURATION
        // AS CHAMADAS ENTRE SEVIÇO VÃO UTILIZAR RSA
        var securityChanels = new SecurityChanelConnections(RsaCryptographySize._2048);
        var temporaryCache = new AppHostTemporaryCache();
        var smtpConfiguration = SmtpConfiguration.GetConfiguration(this.Configuration);

        services.AddSingleton(securityChanels);
        services.AddSingleton(temporaryCache);
        services.AddSingleton<SmtpService>(new SmtpService(smtpConfiguration));
        services.AddScoped<HostCache>();

        JobFactory.CreateJob(() => securityChanels.RemoveChanels(DateTime.UtcNow), 3600000);
        JobFactory.CreateJob(() => temporaryCache.UnsetValue(), 10000);

        services.AddScoped<IHostCache, HostCache>();

        this.ConfigureModule(services);
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        if (this.Annotation is null)
            throw new ApplicationException("DON'T HAVE ANNOTATION MODULE");
        
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<SecurityChanelMiddleware>();

        app.UseEndpoints(a =>
        {
            a.MapPost(string.Concat(this.Annotation.Prefix, "/sec"), async (HttpContext context, [FromServices] SecurityChanelConnections chanels) =>
            {
                try
                {
                    using var reader = new StreamReader(context.Request.Body);
                    var body = await reader.ReadToEndAsync();

                    var clientPublicKey = Binary.FromBase64(body).Bytes;
                    var chanel = chanels.SetChanel(context.Request.Host.Host, clientPublicKey);

                    var rsa = RsaCryptography.Create(new AsymmetricCryptografyKeys()
                    {
                        PublicKey = clientPublicKey,
                        PrivateKey = Array.Empty<byte>()
                    });
                    
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        publicKey = Binary.FromBytes(chanel.ServerPublicKey).toBase64String(),
                        test = rsa.getPublicProvider().Encrypt(Binary.FromString("ping").Bytes)
                    });
                }

                catch (Exception ex)
                {
                    chanels.RemoveChanel(context.Request.Host.Host);
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsJsonAsync(new { security = true, detail = "FAILURE IN VALIDATION", solucion = "PUBLIC KEY IS INVALID, RESEND YOUR PUBLIC KEY FILE" });
                }
            });
            
            a.MapControllerRoute(
                name: this.Module.ToString(),
                pattern: string.Concat(this.Annotation.Prefix, "/{controller}/{action=Index}/{id?}")
            );
        });
    }
}
