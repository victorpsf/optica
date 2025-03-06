using Shared.Databases.Entities;
using Shared.Models.Service;

namespace Authentication.Server;

public partial class AuthenticationController
{
    public async Task SendTryLogin(User user, AuthCode authCode)
    {
        var dateTimeString = authCode.CreatedAt.ToString("yyyy/MM/dd hh:mm");
        
        await this.smtpService.SendAsync(
            SmtpMessageModel.Create()
                .AddTo(user.Email)
                .AddMessage(@$"
<h1>Solicitação de acesso realizado dia {dateTimeString} UTC</h1>
<p>Se esta solicitação foi realizara por você utilize o código {authCode.Code} caso contrario entre em contato com a equipe de suporte da sua empresa, sua conta pode ter sido violada.</p>
<i>Criptografamos seu usuário para sua segurança, se sua conta foi violada realize uma solicitação ao suporte da sua empresa para criar uma nova conta o mais rápido possível.</i>
")
                .AddSubject("Tentativa de Login")
                .AddIsHtml(true)
        );
    }
}