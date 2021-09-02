using Microsoft.AspNetCore.Components.Authorization;
using MyApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp
{
    public class CustomTokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ITokenRepository tokenRepository;
        private readonly IAuthenticationRepository authenticationRepository;

        public CustomTokenAuthenticationStateProvider(ITokenRepository tokenRepository,
            IAuthenticationRepository authenticationRepository)
        {
            this.tokenRepository = tokenRepository;
            this.authenticationRepository = authenticationRepository;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {            
            var userName = await authenticationRepository.GetUserInfoAsync(await tokenRepository.GetToken());
            if (!string.IsNullOrWhiteSpace(userName))
            {
                var claims = new List<Claim>();
                var claim = new Claim(ClaimTypes.Name, userName);
                claims.Add(claim);                

                var identity = new ClaimsIdentity(claims, "Custom Token Auth");
                var principal = new ClaimsPrincipal(identity);

                return new AuthenticationState(principal);
            }
            else
            {             
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }
    }
}
