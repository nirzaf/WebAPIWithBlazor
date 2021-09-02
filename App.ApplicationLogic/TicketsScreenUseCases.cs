using Core.Models;
using MyApp.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public class TicketsScreenUseCases : ITicketsScreenUseCases
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITicketRepository _ticketRepository;

        public TicketsScreenUseCases(IProjectRepository projectRepository,
            ITicketRepository ticketRepository)
        {
            _projectRepository = projectRepository;
            _ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<Ticket>> ViewTickets(int projectId)
        {
            return await _projectRepository.GetProjectTicketsAsync(projectId);
        }

        public async Task<IEnumerable<Ticket>> SearchTickets(string filter)
        {
            if (!int.TryParse(filter, out var ticketId)) return await _ticketRepository.GetAsync(filter);
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            var tickets = new List<Ticket> { ticket };
            return tickets;
        }

        public async Task<IEnumerable<Ticket>> ViewOwnersTickets(int projectId, string ownerName)
        {
            return await _projectRepository.GetProjectTicketsAsync(projectId, ownerName);
        }

        public async Task<Ticket> ViewTicketById(int ticketId)
        {
            return await _ticketRepository.GetByIdAsync(ticketId);
        }

        public async Task UpdateTicket(Ticket ticket)
        {
            await _ticketRepository.UpdateAsync(ticket);
        }
    }
}