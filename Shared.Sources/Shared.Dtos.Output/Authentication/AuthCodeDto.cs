namespace Shared.Dtos.Output.Authentication;

public class AuthCodeDto {
    public int? Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public DateTime ExpireIn { get; set; }
}