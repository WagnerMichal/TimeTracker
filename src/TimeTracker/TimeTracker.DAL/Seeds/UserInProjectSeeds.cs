using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Seeds
{
    public static class UserInProjectSeeds
    {
        public static readonly UserInProjectEntity SimpleUserInProject = new()
        {
            UserID = Guid.Parse("7c6cdd3e-481d-427a-a971-ae306aba8c95"),
            ProjectID = Guid.Parse("7c5cdd3e-481d-427a-a971-ae306aba8c95"),
            ID = Guid.Parse("7c4cdd3e-481d-427a-a971-ae306aba8c95"),
        };

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInProjectEntity>().HasData(
                SimpleUserInProject);
        }
    }
}
