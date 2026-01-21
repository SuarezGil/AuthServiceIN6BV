namespace AuthService.Domain.Enums;
public static class RolConstants
{
    public const string USER_ROLE = "USER_ROLE";
    public const string ADMIN_ROLE = "ADMIN_ROLE";

    public static readonly string[] AllRoles = [USER_ROLE, ADMIN_ROLE];
}

