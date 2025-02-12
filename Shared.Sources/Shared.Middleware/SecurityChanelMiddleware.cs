using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Libraries;
using Shared.Models.Security;
using Shared.Security;

namespace Shared.Middleware;

public class SecurityChanelMiddleware
{
    private readonly RequestDelegate _next;
    
    public SecurityChanelMiddleware(
        RequestDelegate next
    )
        => this._next = next;

    public async Task InvokeAsync(HttpContext context, [FromServices] SecurityChanelConnections chanels)
    {
        var chanel = chanels.GetChanel(context.Request.Host.Host);
        
        if (chanel is not null)
            try
            {
                var rsa = RsaCryptography.Create(new AsymmetricCryptografyKeys()
                {
                    PublicKey = chanel.ClientPublicKey,
                    PrivateKey = chanel.ServerPrivateKey
                });

                var reader = new StreamReader(context.Request.Body);
                var encryptedRequestBody = await reader.ReadToEndAsync();
                var plainTextRequestBody = rsa.getPrivateProvider().Decrypt(Binary.FromBase64(encryptedRequestBody).Bytes);

                context.Request.Body = new MemoryStream(plainTextRequestBody);
                context.Request.Body.Position = 0;
                
                await _next(context);
                
                // context.Response.Body.Seek(0, SeekOrigin.Begin);
                var plainTextReponseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
                var encryptedReponseBody = rsa.getPublicProvider().Encrypt(Binary.FromString(plainTextReponseBody).Bytes);
                context.Response.Body = new MemoryStream(encryptedReponseBody);
                context.Response.Body.Position = 0;
            }

            catch (Exception ex)
            {
                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { security = true, detail = "FAILURE IN COMMUNICATION", solucion = "RESTART SECURITY CHANEL" });
            }
        else await _next(context);
    }
}
