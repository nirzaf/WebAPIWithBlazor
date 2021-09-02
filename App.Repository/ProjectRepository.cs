using MyApp.Repository.ApiClient;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IWebApiExecuter webApiExecuter;

        public ProjectRepository(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }

        public async Task<IEnumerable<Project>> GetAsync()
        {
            return await webApiExecuter.InvokeGet<IEnumerable<Project>>("api/projects");
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await webApiExecuter.InvokeGet<Project>($"api/projects/{id}");
        }

        public async Task<IEnumerable<Ticket>> GetProjectTicketsAsync(int projectId, string filter = null)
        {
            string uri = $"api/projects/{projectId}/tickets";
            if (!string.IsNullOrWhiteSpace(filter))            
                uri += $"?owner={filter}&api-version=2.0";            

            return await webApiExecuter.InvokeGet<IEnumerable<Ticket>>(uri);
        }

        public async Task<int> CreateAsync(Project project)
        {
            project = await webApiExecuter.InvokePost("api/projects", project);
            return project.ProjectId;
        }

        public async Task UpdateAsync(Project project)
        {
            await webApiExecuter.InvokePut($"api/projects/{project.ProjectId}", project);
        }

        public async Task DeleteAsync(int id)
        {
            await webApiExecuter.InvokeDelete($"api/projects/{id}");
        }
    }
}
