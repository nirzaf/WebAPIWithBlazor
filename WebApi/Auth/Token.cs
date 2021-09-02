using System;

namespace WebApi.Auth
{
    public class Token
    {
        public Token(string userName)
        {
            UserName = userName;
            TokenString = Guid.NewGuid().ToString();
            ExpiryDate = DateTime.Now.AddMinutes(10);
        }

        public string TokenString { get; set; }
        public string UserName { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}