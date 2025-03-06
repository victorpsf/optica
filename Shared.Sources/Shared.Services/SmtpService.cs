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
    {
        var client = new SmtpClient(this._configuration.Host, this._configuration.Port);
        client.UseDefaultCredentials = false;
        client.Credentials = GetNetworkCredential();
        client.EnableSsl = this._configuration.UseSsl;
        return client;
    }

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
        try
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
        
        catch (Exception ex) { Console.WriteLine(ex.Message); }
        
        return Task.CompletedTask;
    }
    
    public void Send(SmtpMessageModel model)
    {
        try
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
        
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }
}