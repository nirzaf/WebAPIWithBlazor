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
    [Authorize(policy: "WebApiWriteScope")]
    public class TicketsController : ControllerBase
    {
        private readonly BugsContext db;

        public TicketsController(BugsContext db)
        {
            this.db = db;
        }


        [HttpGet]        
        public async Task<IActionResult> Get()
        {           
            return Ok(await db.Tickets.ToListAsync());
        }

        [HttpGet("{id}")]        
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await db.Tickets.FindAsync(id);
            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        [HttpPost]        
        public async Task<IActionResult> Post([FromBody]Ticket ticket)
        {
            db.Tickets.Add(ticket);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                    new { id = ticket.TicketId },
                    ticket
                );
        }        

        [HttpPut("{id}")]        
        public async Task<IActionResult> Put(int id, [FromBody] Ticket ticket)
        {
            if (id != ticket.TicketId) return BadRequest();

            db.Entry(ticket).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch
            {
                if (await db.Tickets.FindAsync(id) == null)
                    return NotFound();
                throw;
            }
            
            return NoContent();
        }

        [HttpDelete("{id}")]        
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await db.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();

            db.Tickets.Remove(ticket);
            await db.SaveChangesAsync();

            return Ok(ticket);
        }

    }
}
