using MyApp.Repository.ApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IWebApiExecuter webApiExecuter;
        private readonly ITokenRepository tokenRepository;

        public AuthenticationRepository(IWebApiExecuter webApiExecuter, ITokenRepository tokenRepository)
        {
            this.webApiExecuter = webApiExecuter;
            this.tokenRepository = tokenRepository;
        }

        public async Task<string> LoginAsync(string userName, string password)
        {
            var token = await this.webApiExecuter.InvokePostReturnString("authenticate", new { userName = userName, password = password });
            await tokenRepository.SetToken(token);
            if (string.IsNullOrWhiteSpace(token) || token == "\"\"") return null;

            return token;
        }

        public async Task<string> GetUserInfoAsync(string token)
        {
            var userName = await this.webApiExecuter.InvokePostReturnString("getuserinfo", new { token = token });
            if (string.IsNullOrWhiteSpace(userName) || userName == "\"\"") return null;

            return userName;
        }
    }
}
