using Shared.Models.Annotation;

namespace Shared.Dtos.Output.Authentication;

public class UserDto {
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public bool Active { get; set; }
    public bool ForcePasswordReset { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    [ConverterAnnotation(type: ConverterAnnotationType.LIST, reference: typeof(UserRoleDto))]
    public List<UserRoleDto>? Roles { get; set; }
}