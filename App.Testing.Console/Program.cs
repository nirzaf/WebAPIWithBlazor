using MyApp.Repository;
using MyApp.Repository.ApiClient;
using Core.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

HttpClient httpClient = new();
IWebApiExecuter apiExectuer = new WebApiExecuter("https://localhost:44314", httpClient, new TokenRepository(null));

//await TestProjects();

await TestTickets();

#region Test Projects

async Task TestProjects()
{
    Console.WriteLine("////////////////////");
    Console.WriteLine("Reading projects...");
    await GetProjects();

    Console.WriteLine("////////////////////");
    Console.WriteLine("Reading project tickets...");
    await GetProjectTickets(1);

    Console.WriteLine("////////////////////");
    Console.WriteLine("Create a project...");
    var pId = await CreateProject();
    await GetProjects();

    Console.WriteLine("////////////////////");
    Console.WriteLine("Update a project...");
    var project = await GetProject(pId);
    await UpdateProject(project);
    await GetProjects();

    Console.WriteLine("////////////////////");
    Console.WriteLine("Delete a project...");
    await DeleteProject(pId);
    await GetProjects();
}

async Task GetProjects()
{
    ProjectRepository repository = new(apiExectuer);
    var projects = await repository.GetAsync();
    foreach (var project in projects)
    {
        Console.WriteLine($"Project: {project.Name}");
    }
}

async Task<Project> GetProject(int id)
{
    ProjectRepository repository = new(apiExectuer);
    return await repository.GetByIdAsync(id);
}

async Task GetProjectTickets(int id)
{
    var project = await GetProject(id);
    Console.WriteLine($"Project: {project.Name}");

    ProjectRepository repository = new(apiExectuer);
    var tickets = await repository.GetProjectTicketsAsync(id);
    foreach (var ticket in tickets)
    {
        Console.WriteLine($"    Ticket: {ticket.Title}");
    }
}

async Task<int> CreateProject()
{
    var project = new Project { Name = "Another Project" };
    ProjectRepository repository = new(apiExectuer);
    return await repository.CreateAsync(project);
}

async Task UpdateProject(Project project)
{
    ProjectRepository repository = new(apiExectuer);
    project.Name += " updated";
    await repository.UpdateAsync(project);
}

async Task DeleteProject(int id)
{
    ProjectRepository repository = new(apiExectuer);
    await repository.DeleteAsync(id);
}

#endregion

#region Test Tickets
async Task TestTickets()
{
    Console.WriteLine("////////////////////");
    Console.WriteLine("Reading all tickets...");
    await GetTickets();

    Console.WriteLine("////////////////////");
    Console.WriteLine("Reading contains 1...");
    await GetTickets("1");

    Console.WriteLine("////////////////////");
    Console.WriteLine("Create a ticket...");
    var tId = await CreateTicket();
    await GetTickets();

    Console.WriteLine("////////////////////");
    Console.WriteLine("Update a ticket...");
    var ticket = await GetTicketById(tId);
    await UpdateTicket(ticket);
    await GetTickets();

    Console.WriteLine("////////////////////");
    Console.WriteLine("Delete a ticket...");
    await DeleteTicket(tId);
    await GetTickets();
}

async Task GetTickets(string filter = null)
{
    TicketRepository ticketRepository = new(apiExectuer);
    var tickets = await ticketRepository.GetAsync(filter);
    foreach (var ticket in tickets)
    {
        Console.WriteLine($"Ticket: {ticket.Title}");
    }
}

async Task<Ticket> GetTicketById(int id)
{
    TicketRepository ticketRepository = new(apiExectuer);
    var ticket = await ticketRepository.GetByIdAsync(id);
    return ticket;
}

async Task<int> CreateTicket()
{
    TicketRepository ticketRepository = new(apiExectuer);
    return await ticketRepository.CreateAsync(new Ticket
    {
        ProjectId = 2,
        Title = "This a very difficult.",
        Description = "Something is wrong on the server."
    });
}

async Task UpdateTicket(Ticket ticket)
{
    TicketRepository ticketRepository = new(apiExectuer);
    ticket.Title += " Updated";
    await ticketRepository.UpdateAsync(ticket);
}

async Task DeleteTicket(int id)
{
    TicketRepository ticketRepository = new(apiExectuer);
    await ticketRepository.DeleteAsync(id);
}

#endregion