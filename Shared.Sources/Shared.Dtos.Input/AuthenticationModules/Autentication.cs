using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.Input.AuthenticationModules;

public class Autentication: AuthenticationName
{

    [Length(minimumLength: 0, maximumLength: 400)]
    public string? Password { get; set; }
    [Required]
    public int? EnterpriseId { get; set; }
}