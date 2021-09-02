using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Repository
{
    public interface ITicketRepository
    {
        Task<int> CreateAsync(Ticket ticket);
        Task DeleteAsync(int id);
        Task<IEnumerable<Ticket>> GetAsync(string filter = null);
        Task<Ticket> GetByIdAsync(int id);
        Task UpdateAsync(Ticket ticket);
    }
}