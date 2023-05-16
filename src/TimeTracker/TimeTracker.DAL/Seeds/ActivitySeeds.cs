using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Entities;
using System.Globalization;
using TimeTracker.Common.Enums;

namespace TimeTracker.DAL.Seeds;

public static class ActivitySeeds
{
    public static readonly ActivityEntity SimpleActivity = new()
    {
        Start = DateTime.Parse("2023-01-01 07:01:09"),
        End = DateTime.Parse("2023-03-29 16:00:01"),
        Type = Types.design,
        Description = "Random stuff",
        ID = Guid.Parse("7c6cdd3e-481d-427a-a971-ae306aba8c95"),
        UserID = Guid.Parse("00000000-481d-427a-a971-ae306aba8c95"),
        ProjectID = Guid.Parse("11111111-481d-427a-a971-ae306aba8c95")
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityEntity>().HasData(
            SimpleActivity);
    }
}
