using MyApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public class AuthenticationUseCases : IAuthenticationUseCases
    {
        private readonly IAuthenticationRepository authenticationRepository;
        private readonly ITokenRepository tokenRepository;

        public AuthenticationUseCases(IAuthenticationRepository authenticationRepository,
            ITokenRepository tokenRepository)
        {
            this.authenticationRepository = authenticationRepository;
            this.tokenRepository = tokenRepository;
        }

        public async Task<string> LoginAsync(string userName, string password)
        {
            return await authenticationRepository.LoginAsync(userName, password);
        }

        public async Task<string> GetUserInfoAsync(string token)
        {
            return await authenticationRepository.GetUserInfoAsync(token);
        }

        public async Task Logout()
        {
            await tokenRepository.SetToken(string.Empty);
        }
    }
    
}
