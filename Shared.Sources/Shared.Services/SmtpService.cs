using System.Net;
using System.Net.Mail;
using System.Text;
using Shared.Interfaces.Configurations;
using Shared.Models.Service;

namespace Shared.Services;

public class SmtpService
{
    private readonly ISmtpConfiguration _configuration;
    
    public SmtpService(ISmtpConfiguration configuration)
    { this._configuration = configuration; }
    
    private NetworkCredential GetNetworkCredential()
        => new ()
        {
            UserName = _configuration.Username,
            Password = _configuration.Password,
        };

    private MailAddress GetMailAddress(string address)
        => new(address);

    private SmtpClient CreateClient()
        => new SmtpClient
        {
            Host = this._configuration.Host,
            Port = this._configuration.Port,
            UseDefaultCredentials = false,
            Credentials = GetNetworkCredential(),
            EnableSsl = this._configuration.UseSsl
        };

    private MailMessage CreateMailMessage(SmtpMessageModel model)
    {
        var message = new MailMessage()
        {
            From = GetMailAddress(this._configuration.Username)
        };

        foreach (var to in model.To)
            message.To.Add(GetMailAddress(to));
        
        return message;
    }
    
    public Task SendAsync(SmtpMessageModel model)
    {
        var client = this.CreateClient();
        var message = this.CreateMailMessage(model);
        
        message.Subject = model.Subject;
        message.SubjectEncoding = Encoding.UTF8;
        message.Body = model.Message;
        message.BodyEncoding = Encoding.UTF8;
        message.IsBodyHtml = model.IsHtml;
        
        client.Send(message);
        return Task.CompletedTask;
    }
    
    public void Send(SmtpMessageModel model)
    {
        var client = this.CreateClient();
        var message = this.CreateMailMessage(model);
        
        message.Subject = model.Subject;
        message.SubjectEncoding = Encoding.UTF8;
        message.Body = model.Message;
        message.BodyEncoding = Encoding.UTF8;
        message.IsBodyHtml = model.IsHtml;
        
        client.Send(message);
    }
}