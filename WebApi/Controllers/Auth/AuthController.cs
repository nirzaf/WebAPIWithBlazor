using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Auth;

namespace WebApi.Controllers.Auth
{
    [ApiController]    
    public class AuthController : ControllerBase
    {
        private readonly ICustomUserManager customUserManager;
        private readonly ICustomTokenManager customTokenManager;

        public AuthController(ICustomUserManager customUserManager,
            ICustomTokenManager customTokenManager)
        {
            this.customUserManager = customUserManager;
            this.customTokenManager = customTokenManager;
        }

        [HttpPost]
        [Route("/authenticate")]
        public Task<string> Authenticate(UserCredential userCredential)
        {
            return Task.FromResult(customUserManager.Authenticate(userCredential.userName, userCredential.password));
        }

        [HttpPost]
        [Route("/verifytoken")]
        public Task<bool> Verify(Token token)
        {
            return Task.FromResult(customTokenManager.VerifyToken(token.token));
        }

        [HttpPost]
        [Route("/getuserinfo")]
        public Task<string> GetUserInfoByToken(Token token)
        {
            return Task.FromResult(customTokenManager.GetUserInfoByToken(token.token));
        }
    }

    public class UserCredential
    {
        public string userName { get; set; }
        public string password { get; set; }
    }

    public class Token
    {
        public string token { get; set; }
    }
}
