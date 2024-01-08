using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Infrastructure.Persistence
{
    public class ProjectManagerDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }

        public ProjectManagerDbContext(DbContextOptions<ProjectManagerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
