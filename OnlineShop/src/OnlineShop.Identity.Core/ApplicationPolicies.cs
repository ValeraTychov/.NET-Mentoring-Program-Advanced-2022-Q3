using Microsoft.AspNetCore.Authorization;

namespace OnlineShop.Identity.Core;

public class ApplicationPolicies
{
    public static AuthorizationPolicy AdministratorRoleRequired { get; } = CreatePolicy(builder => builder.RequireRole(ApplicationRoles.Administrator));

    public static AuthorizationPolicy CanCreateCrudClaimRequired { get; } =
        CreatePolicy(builder => builder.RequireClaim(ApplicationClaims.CrudType, ApplicationClaims.CanCreate));

    public static AuthorizationPolicy CanReadCrudClaimRequired { get; } =
        CreatePolicy(builder => builder.RequireClaim(ApplicationClaims.CrudType, ApplicationClaims.CanRead));

    public static AuthorizationPolicy CanUpdateCrudClaimRequired { get; } =
        CreatePolicy(builder => builder.RequireClaim(ApplicationClaims.CrudType, ApplicationClaims.CanUpdate));

    public static AuthorizationPolicy CanDeleteCrudClaimRequired { get; } =
        CreatePolicy(builder => builder.RequireClaim(ApplicationClaims.CrudType, ApplicationClaims.CanDelete));

    private static AuthorizationPolicy CreatePolicy(Func<AuthorizationPolicyBuilder, AuthorizationPolicyBuilder> builder)
    {
        return builder(new AuthorizationPolicyBuilder()).Build();
    }
}