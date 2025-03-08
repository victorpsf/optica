using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.Input.AuthenticationModules;

public class AuthenticationCode {
    [Required]
    [Length(minimumLength: 9, maximumLength: 9)]
    public string Code { get; set; } = string.Empty;
}