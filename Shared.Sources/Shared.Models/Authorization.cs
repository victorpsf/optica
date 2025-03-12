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
    
    public enum AuthenticationPermission {
        AUTHENTICACAO_VISUALIZAR,
        AUTHENTICACAO_MANTER,
        AUTHENTICACAO_VINCULACAO
    }
    public enum EnterprisePermission
    {
        EMPRESA_VISUALIZAR,
        EMPRESA_MANTER
    }
    public enum PersonalPermission {
        PESSOAL_VISUALIZAR,
        PESSOAL_MANTER
    }
    public enum FincancialPermission {}
}