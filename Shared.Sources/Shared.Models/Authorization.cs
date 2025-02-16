namespace Shared.Models;

public class Authorization
{
    public static List<string> AllAuthorizations()
    {
        var authorizations = new List<string>();
        foreach (Type enumType in typeof(Authorization).GetNestedTypes())
            if (enumType.IsEnum) authorizations.AddRange(Enum.GetNames(enumType));
        return authorizations;
    }
    
    public enum AuthenticationPermission {}
    public enum PersonalPermission { }
    public enum FincancialPermission {}
    public enum EnterprisePermission {}
}