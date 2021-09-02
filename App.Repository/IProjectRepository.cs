using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Repository
{
    public interface IProjectRepository
    {
        Task<int> CreateAsync(Project project);
        Task DeleteAsync(int id);
        Task<IEnumerable<Project>> GetAsync();
        Task<Project> GetByIdAsync(int id);        
        Task<IEnumerable<Ticket>> GetProjectTicketsAsync(int projectId, string filter = null);
        Task UpdateAsync(Project project);
    }
}