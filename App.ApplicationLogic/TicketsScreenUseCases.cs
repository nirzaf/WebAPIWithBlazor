﻿using Core.Models;
using MyApp.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public class TicketsScreenUseCases : ITicketsScreenUseCases
    {
        private readonly IProjectRepository projectRepository;
        private readonly ITicketRepository ticketRepository;

        public TicketsScreenUseCases(IProjectRepository projectRepository,
            ITicketRepository ticketRepository)
        {
            this.projectRepository = projectRepository;
            this.ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<Ticket>> ViewTickets(int projectId)
        {
            return await projectRepository.GetProjectTicketsAsync(projectId);
        }

        public async Task<IEnumerable<Ticket>> SearchTickets(string filter)
        {
            if (!int.TryParse(filter, out var ticketId)) return await ticketRepository.GetAsync(filter);
            var ticket = await ticketRepository.GetByIdAsync(ticketId);
            var tickets = new List<Ticket> { ticket };
            return tickets;
        }

        public async Task<IEnumerable<Ticket>> ViewOwnersTickets(int projectId, string ownerName)
        {
            return await projectRepository.GetProjectTicketsAsync(projectId, ownerName);
        }

        public async Task<Ticket> ViewTicketById(int ticketId)
        {
            return await ticketRepository.GetByIdAsync(ticketId);
        }

        public async Task UpdateTicket(Ticket ticket)
        {
            await ticketRepository.UpdateAsync(ticket);
        }
    }
}