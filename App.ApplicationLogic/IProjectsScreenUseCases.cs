using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public interface IProjectsScreenUseCases
    {
        Task<IEnumerable<Project>> ViewProjectsAsync();
    }
}