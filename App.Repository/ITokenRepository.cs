using System.Threading.Tasks;

namespace MyApp.Repository
{
    public interface ITokenRepository
    {
        Task<string> GetToken();
        Task SetToken(string token);
    }
}