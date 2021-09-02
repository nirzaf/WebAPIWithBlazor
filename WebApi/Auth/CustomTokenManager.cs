using System.Collections.Generic;
using System.Linq;

namespace WebApi.Auth
{
    public class CustomTokenManager : ICustomTokenManager
    {
        private List<Token> tokens = new();

        public string CreateToken(string userName)
        {
            var token = new Token(userName);
            tokens.Add(token);

            return token.TokenString;
        }

        public bool VerifyToken(string token)
        {
            return tokens.Any(x => token != null && token.Contains(x.TokenString));
        }

        public string GetUserInfoByToken(string tokenString)
        {
            var token = tokens.FirstOrDefault(x => tokenString != null && tokenString.Contains(x.TokenString));
            if (token != null) return token.UserName;

            return string.Empty;
        }
    }
}