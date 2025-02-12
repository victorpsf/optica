using System.ComponentModel.DataAnnotations;

namespace Authentication.Server.Requests;

public class Autentication
{
    [Length(minimumLength: 0, maximumLength: 500)]
    public string? Name { get; set; }
    [Length(minimumLength: 0, maximumLength: 500)]
    public string? Email { get; set; }
    [Length(minimumLength: 0, maximumLength: 400)]
    public string? Password { get; set; }
}