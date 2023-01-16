// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Duende.IdentityServer.Models;
using OnlineShop.Identity.Core;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("catalog", "Catalog", new []{ ApplicationClaims.CrudType })
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("api1", "My API", new List<string>{ "foo" , "bar" })
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "api1" },
                AlwaysSendClientClaims = true,
                AlwaysIncludeUserClaimsInIdToken = true,

            },
            new Client
            {
                ClientId = "web",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:7272/callback" },
                PostLogoutRedirectUris = { "https://localhost:7272/" },
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "catalog" }
            },
        };
}
