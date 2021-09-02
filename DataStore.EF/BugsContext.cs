using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataStore.EF
{
    public class BugsContext : DbContext
    {
        public BugsContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Tickets)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId);

            //seeding
            modelBuilder.Entity<Project>().HasData(
                new Project { ProjectId = 1, Name = "Project 1" },
                new Project { ProjectId = 2, Name = "Project 2" }
            );

            modelBuilder.Entity<Ticket>().HasData(
                new Ticket
                {
                    TicketId = 1,
                    Title = "Bug #1",
                    ProjectId = 1,
                    Owner = "Frank Liu",
                    ReportDate = new DateTime(2021, 1, 1),
                    DueDate = new DateTime(2021, 2, 1)
                },
                new Ticket
                {
                    TicketId = 2,
                    Title = "Bug #2",
                    ProjectId = 1,
                    Owner = "Frank Liu",
                    ReportDate = new DateTime(2021, 1, 1),
                    DueDate = new DateTime(2021, 2, 1)
                },
                new Ticket
                {
                    TicketId = 5,
                    Title = "Swagger bug",
                    ProjectId = 1,
                    Owner = "Tomas Liu",
                    ReportDate = new DateTime(2021, 1, 1),
                    DueDate = new DateTime(2021, 2, 1)
                },
                new Ticket { TicketId = 3, Title = "Bug #3", ProjectId = 2 },
                new Ticket { TicketId = 4, Title = "Another bug", Description = "A new bug", ProjectId = 2 },
                new Ticket
                {
                    TicketId = 6,
                    Title = "Another Swagger bug",
                    ProjectId = 2,
                    Owner = "Steve Liu",
                    ReportDate = new DateTime(2021, 1, 1),
                    DueDate = new DateTime(2021, 2, 1)
                }
            );
        }
    }
}