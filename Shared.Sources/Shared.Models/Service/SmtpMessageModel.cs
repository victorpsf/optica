namespace Shared.Models.Service;

public class SmtpMessageModel
{
    public List<string> To { get; } = new();
    public string Subject { get; private set; } = string.Empty;
    public string Message { get; private set; } = string.Empty;
    public bool IsHtml { get; private set; } = false;
    
    private SmtpMessageModel() { }

    public static SmtpMessageModel Create()
        => new SmtpMessageModel();

    public SmtpMessageModel AddTo(params string[] to)
    { 
        this.To.AddRange(to);
        return this;
    }

    public SmtpMessageModel AddSubject(string subject)
    {
        this.Subject = subject;
        return this;
    }

    public SmtpMessageModel AddMessage(string message)
    {
        this.Message = message;
        return this;
    }

    public SmtpMessageModel AddIsHtml(bool isHtml)
    {
        this.IsHtml = isHtml;
        return this;
    }
}