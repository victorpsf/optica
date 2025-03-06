using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Databases.DbContexts;
using Shared.Databases.Services;
using Shared.Interfaces.Configurations;
using Shared.Interfaces.Databases;
using Shared.Libraries;
using Shared.Middleware;
using Shared.Models.Configurations;
using Shared.Models.Service.Modules;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using AuthorizationMiddleware = Shared.Middleware.AuthorizationMiddleware;

namespace Shared.Configuration;

public partial class StartupCoreConfiguration
{
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

        services.AddScoped<IAuthorizationHandler, AuthorizationMiddleware>();
        services.AddAuthorization(
            x =>
            {
                x.FallbackPolicy = x.DefaultPolicy;
                foreach (string authorizationName in Authorization.AllAuthorizations())
                    x.AddPolicy(authorizationName, policy => policy.AddRequirements(new AuthorizationRequirements(authorizationName)));
            }
        );
    }
    
    protected void ConfigureAuthentication(IServiceCollection services)
    {
        var configuration = AuthenticationConfiguration.GetConfiguration(this.Configuration);

        services.AddSingleton(configuration);
        this.ConfigureSecurity(
            services, 
            configuration.Security
        );
        services.AddDbContext<AuthenticationDbContext>(options => options.UseNpgsql(configuration.ConnectionString));
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthCodeService, AuthCodeService>();
        services.AddScoped<AuthenticatedUser>();
    }
    
    protected void ConfigureEnterprise(IServiceCollection services)
    {
        var configuration = EnterpriseConfiguration.GetConfiguration(this.Configuration);
        services.AddSingleton(configuration);
        this.ConfigureSecurity(
            services, 
            configuration.Security
        );
        // services.AddDbContext<EnterpriseDbContext>(options => options.UseMySQL(configuration.ConnectionString));
    }

    protected void ConfigureFinancial(IServiceCollection services)
    {
        var configuration = FinancialConfiguration.GetConfiguration(this.Configuration);
        services.AddSingleton(configuration);
        this.ConfigureSecurity(
            services, 
            configuration.Security
        );
        // services.AddDbContext<FinancialDbContext>(options => options.UseMySQL(configuration.ConnectionString));
    }
    
    protected void ConfigurePersonal(IServiceCollection services)
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
        this.ConfigureAuthentication(services);
        
        switch (this.Module) {
            case ModuleNames.AUTHENTICATION: // IS DEFAULT MODULE
                break;
            case ModuleNames.FINANCIAL:
                ConfigureFinancial(services);
                break;
            case ModuleNames.ENTERPRISES:
                ConfigureEnterprise(services);
                break;
            case ModuleNames.PERSONAL:
                ConfigurePersonal(services);
                break;
            default:
                throw new NotImplementedException();
        }
    }
}