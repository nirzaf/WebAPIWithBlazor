namespace WebApi.Auth
{
    public interface ICustomTokenManager
    {
        string CreateToken(string userName);
        string GetUserInfoByToken(string token);
        bool VerifyToken(string token);
    }
}