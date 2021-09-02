using MyApp.Repository;
using Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public class ProjectsScreenUseCases : IProjectsScreenUseCases
    {
        private readonly IProjectRepository projectRepository;

        public ProjectsScreenUseCases(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public async Task<IEnumerable<Project>> ViewProjectsAsync()
        {
            return await projectRepository.GetAsync();
        }
    }
}
