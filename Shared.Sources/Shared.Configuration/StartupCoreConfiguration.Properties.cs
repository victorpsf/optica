using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Databases.DbContexts;
using Shared.Databases.Services;
using Shared.Interfaces.Configurations;
using Shared.Interfaces.Databases;
using Shared.Libraries;
using Shared.Middleware;
using Shared.Models.Annotation;
using Shared.Models.Configurations;
using Shared.Models.Service.Modules;

namespace Shared.Configuration;

public partial class StartupCoreConfiguration
{
    protected IConfiguration Configuration { get; private set; }
    protected ModuleNames Module { get; private set; }
    protected ModuleAnnotation? Annotation { get; private set; }
    
    protected void ConfigureConfigurationAuthentication(IServiceCollection services)
    {
        var configuration = AuthenticationConfiguration.GetConfiguration(this.Configuration);

        services.AddSingleton(configuration);
        this.ConfigureSecurity(
            services, 
            configuration.Security
        );
        services.AddDbContext<AuthenticationDbContext>(options => options.UseNpgsql(configuration.ConnectionString));
        services.AddScoped<IAuthenticationService, AuthenticationService>();
    }

    protected void ConfigureConfigurationEnterprise(IServiceCollection services)
    {
        var configuration = EnterpriseConfiguration.GetConfiguration(this.Configuration);
        services.AddSingleton(configuration);
        this.ConfigureSecurity(
            services, 
            configuration.Security
        );
        // services.AddDbContext<EnterpriseDbContext>(options => options.UseMySQL(configuration.ConnectionString));
    }

    protected void ConfigureConfigurationFinancial(IServiceCollection services)
    {
        var configuration = FinancialConfiguration.GetConfiguration(this.Configuration);
        services.AddSingleton(configuration);
        this.ConfigureSecurity(
            services, 
            configuration.Security
        );
        // services.AddDbContext<FinancialDbContext>(options => options.UseMySQL(configuration.ConnectionString));
    }
    
    protected void ConfigureConfigurationPersonal(IServiceCollection services)
    {
        var configuration = PersonalConfiguration.GetConfiguration(this.Configuration);
        services.AddSingleton(configuration);
        this.ConfigureSecurity(
            services, 
            configuration.Security
        );
        // services.AddDbContext<FinancialDbContext>(options => options.UseMySQL(configuration.ConnectionString));
    }

    protected void ConfigureModule(IServiceCollection services)
    {
        switch (this.Module) {
            case ModuleNames.AUTHENTICATION:
                ConfigureConfigurationAuthentication(services);
                break;
            case ModuleNames.FINANCIAL:
                ConfigureConfigurationFinancial(services);
                break;
            case ModuleNames.ENTERPRISES:
                ConfigureConfigurationEnterprise(services);
                break;
            case ModuleNames.PERSONAL:
                ConfigureConfigurationPersonal(services);
                break;
            default:
                throw new NotImplementedException();
        }
    }
    
    private void ConfigureSecurity(
        IServiceCollection services,
        ISecurityConfiguration securityConfiguration
    ) {
        services.AddSingleton(securityConfiguration);
        services.AddScoped<AuthenticatedUser>();

        services.AddAuthentication(
            x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
        ).AddJwtBearer(
            JwtBearerDefaults.AuthenticationScheme,
            x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Binary.FromBase64(securityConfiguration.TokenSecret).Bytes),
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidIssuer = securityConfiguration.Issuer
                };
            }
        );
    }
}