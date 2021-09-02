using Core.Models;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public interface ITicketScreenUseCases
    {
        Task<int> AddTicket(Ticket ticket);
        Task DeleteTicket(int ticketId);
    }
}