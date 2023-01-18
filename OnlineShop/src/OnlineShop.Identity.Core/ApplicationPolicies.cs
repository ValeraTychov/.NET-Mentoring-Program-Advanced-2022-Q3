using Microsoft.AspNetCore.Authorization;

namespace OnlineShop.Identity.Core;

public static class ApplicationPolicies
{
    public static AuthorizationPolicy AdministratorRoleRequired { get; } = CreatePolicy(builder => builder.RequireRole(ApplicationRoles.Administrator));

    public static AuthorizationPolicy CreateAllowed { get; } =
        CreatePolicy(builder => builder.RequireClaim(ApplicationClaims.CrudType, ApplicationClaims.CanCreate));

    public static AuthorizationPolicy ReadAllowed { get; } =
        CreatePolicy(builder => builder.RequireClaim(ApplicationClaims.CrudType, ApplicationClaims.CanRead));

    public static AuthorizationPolicy UpdateAllowed { get; } =
        CreatePolicy(builder => builder.RequireClaim(ApplicationClaims.CrudType, ApplicationClaims.CanUpdate));

    public static AuthorizationPolicy DeleteAllowed { get; } =
        CreatePolicy(builder => builder.RequireClaim(ApplicationClaims.CrudType, ApplicationClaims.CanDelete));

    private static AuthorizationPolicy CreatePolicy(Func<AuthorizationPolicyBuilder, AuthorizationPolicyBuilder> builder)
    {
        return builder(new AuthorizationPolicyBuilder()).Build();
    }
}
