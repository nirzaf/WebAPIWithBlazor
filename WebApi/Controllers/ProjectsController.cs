using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Filters;

namespace PlatformDemo.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    //[CustomTokenAuthFilter]    
    [Authorize(policy: "WebApiScope")]
    public class ProjectsController : ControllerBase
    {
        private readonly BugsContext db;

        public ProjectsController(BugsContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await db.Projects.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await db.Projects.FindAsync(id);
            if (project == null)
                return NotFound();

            return Ok(project);
        }
        
        [HttpGet]
        [Route("/api/projects/{pid}/tickets")]
        public async Task<IActionResult> GetProjectTickets(int pId)
        {
            var tickets = await db.Tickets.Where(t => t.ProjectId == pId).ToListAsync();
            if (tickets == null || tickets.Count <= 0)
                return NotFound();

            return Ok(tickets);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Project project)
        {
            db.Projects.Add(project);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                    new { id = project.ProjectId },
                    project
                );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Project project)
        {
            if (id != project.ProjectId) return BadRequest();

            db.Entry(project).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch
            {
                if (db.Projects.Find(id) == null)
                    return NotFound();
                throw;
            }
            

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await db.Projects.FindAsync(id);
            if (project == null) return NotFound();

            db.Projects.Remove(project);
            await db.SaveChangesAsync();

            return Ok(project);
        }

    }
}
