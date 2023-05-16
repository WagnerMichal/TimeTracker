using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Seeds
{
    public static class ProjectSeeds
    {
        public static readonly ProjectEntity SimpleProject = new()
        {
            Name = "Project 1",
            ID = Guid.Parse("11111111-481d-427a-a971-ae306aba8c95")
        };

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectEntity>().HasData(
                SimpleProject);
        }
    }
}
