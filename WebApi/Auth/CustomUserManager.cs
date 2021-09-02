using System.Collections.Generic;

namespace WebApi.Auth
{
    public class CustomUserManager : ICustomUserManager
    {
        private Dictionary<string, string> credentials = new()
        {
            { "frank", "password" },
            { "tom", "password1" }
        };

        private readonly ICustomTokenManager customTokenManager;

        public CustomUserManager(ICustomTokenManager customTokenManager)
        {
            this.customTokenManager = customTokenManager;
        }

        public string Authenticate(string userName, string password)
        {
            //validate the credentials              
            if (!string.IsNullOrWhiteSpace(userName) && credentials[userName] != password) return string.Empty;

            //generate token
            return customTokenManager.CreateToken(userName);
        }
    }
}