// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            { 
                new ApiScope("webapi"),
                new ApiScope("read"),
                new ApiScope("write")
            };

        public static IEnumerable<Client> Clients =>
            new Client[] 
            { 
                new Client
                {
                    ClientId = "console.client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = { "webapi", "write" }
                },
                new Client
                {
                    ClientId = "blazorwasm.client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,

                    RedirectUris = {"https://localhost:44393/authentication/login-callback" },
                    PostLogoutRedirectUris = {"https://localhost:44393/authentication/logout-callback" },
                    AllowedCorsOrigins = {"https://localhost:44393" },

                    AllowedScopes = 
                    { 
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "webapi", 
                        "write"
                    }

                    
                }                
            };
    }
}