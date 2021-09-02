using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public interface ITicketsScreenUseCases
    {
        Task<IEnumerable<Ticket>> SearchTickets(string filter);
        Task UpdateTicket(Ticket ticket);
        Task<IEnumerable<Ticket>> ViewOwnersTickets(int projectId, string ownerName);
        Task<Ticket> ViewTicketById(int ticketId);
        Task<IEnumerable<Ticket>> ViewTickets(int projectId);
    }
}