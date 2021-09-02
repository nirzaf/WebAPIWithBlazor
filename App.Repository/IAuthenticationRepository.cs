using System.Threading.Tasks;

namespace MyApp.Repository
{
    public interface IAuthenticationRepository
    {
        Task<string> GetUserInfoAsync(string token);
        Task<string> LoginAsync(string userName, string password);
    }
}