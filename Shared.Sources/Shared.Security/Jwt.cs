using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using Shared.Interfaces.Configurations;
using Shared.Interfaces.Security;
using Shared.Libraries;
using Shared.Models.Security;

namespace Shared.Security;

public class Jwt: IJwt
{
    private ISecurityConfiguration SecurityConfiguration;
    private static string TokenType = "Bearer";

    public Jwt(ISecurityConfiguration securityConfiguration)
        => this.SecurityConfiguration = securityConfiguration;

    public static Jwt Create(ISecurityConfiguration securityConfiguration)
        => new(securityConfiguration);

    public SymmetricSecurityKey SymmetricSecurityKey { get => new(Binary.FromBase64(this.SecurityConfiguration.TokenSecret).Bytes); }
    public SigningCredentials SigningCredentials { get => new(this.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature); }

    public IAuthToken Write(IClaimIdentifier claim)
    {
        var now = DateTime.Now;
        var handler = new JwtSecurityTokenHandler();

        return new AuthToken()
        {
            Token = handler.WriteToken(
                handler.CreateToken(
                    new SecurityTokenDescriptor()
                    {
                        Subject = new ClaimsIdentity(
                            new Claim[] {
                                new("uI", claim.UserId.ToString()),
                                new("uR", JsonSerializer.Serialize(claim.Roles)),
                                new("uP", JsonSerializer.Serialize(claim.Permissions)),
                                new("uE", claim.EnterpriseId.ToString())
                            }
                        ),
                        Issuer = this.SecurityConfiguration.Issuer,
                        TokenType = TokenType,
                        Expires = now.AddMinutes(this.SecurityConfiguration.TokenMinutes),
                        SigningCredentials = this.SigningCredentials
                    })
            ),
            Type = TokenType
        };
    }

    public IClaimIdentifier Read(string token)
    {
        try
        {
            (new JwtSecurityTokenHandler())
                .ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidIssuer = this.SecurityConfiguration.Issuer,
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        IssuerSigningKey = this.SymmetricSecurityKey,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true
                    },
                    out SecurityToken validatedToken
                );

            var jwt = (JwtSecurityToken)validatedToken;
            return ClaimIdentifier.GetInstance(jwt.Claims);
        }
        catch (Exception error)
        { throw new UnauthorizedAccessException($"INVALID TOKEN {error.Message}", error); }
    }
}