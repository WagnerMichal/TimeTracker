using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Seeds
{
    public static class UserSeeds
    {
        public static readonly UserEntity SimpleUser = new()
        {
            Name = "Jan",
            LastName = "Novak",
            Photo = "https://www.flaticon.com/free-icon/user_1946429",
            ID = Guid.Parse("00000000-481d-427a-a971-ae306aba8c95")
        };

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasData(
                SimpleUser);
        }
    }
}
