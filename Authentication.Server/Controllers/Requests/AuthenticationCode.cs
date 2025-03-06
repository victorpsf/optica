using System.ComponentModel.DataAnnotations;

namespace Authentication.Server.Controllers.Requests;

public class AuthenticationCode
{
    [Required]
    [Length(minimumLength: 9, maximumLength: 9)]
    public string Code { get; set; } = string.Empty;
}
