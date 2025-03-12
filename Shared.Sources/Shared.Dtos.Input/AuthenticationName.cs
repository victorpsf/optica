using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.Input;

public class AuthenticationName
{
    [Length(minimumLength: 0, maximumLength: 500)]
    public string? Name { get; set; }
    [Length(minimumLength: 0, maximumLength: 500)]
    public string? Email { get; set; }
}
