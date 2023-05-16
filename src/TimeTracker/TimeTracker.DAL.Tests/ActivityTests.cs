using TimeTracker.Common.Tests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Seeds;
//using TimeTracker.Common.Tests.Factories;
using TimeTracker.DAL.Entities;
using Xunit.Abstractions;
using Microsoft.Data.SqlClient;
using TimeTracker.Common.Enums;

namespace TimeTracker.DAL.Tests
{
    public class ActivityTests : DbContextTestsBase
    {
        public ActivityTests(ITestOutputHelper output) : base(output)
        {
        }

        //public ActivityTests()
        //{
        //    var a = new DbContextLocalDBTestingFactory("timetracker");
        //    _dbContextSUT = a.CreateDbContext();
        //}
        [Fact]
        public async Task NewActivityAdded()
        {
            // Create a new project and add it to the context
            var project = new ProjectEntity
            {
                ID = Guid.Parse("C01D69AC-4B3B-4A7A-A47F-58C7E3A3A2F7"),
                Name = "Test Project"
            };
            TimeTrackerDbContextSUT.Projects.Add(project);

            // Create a new user and add it to the context
            var user = new UserEntity
            {
                ID = Guid.Parse("A19C2C03-998C-4736-9A8E-03A7C121BB9C"),
                Name = "Test User",
                LastName = "1111",
                Photo = "www.proctonejede.cz"
            };
            TimeTrackerDbContextSUT.Users.Add(user);

            // Save changes to the context to persist the new project and user
            await TimeTrackerDbContextSUT.SaveChangesAsync();

            // Create a new activity and add it to the context
            var activity = new ActivityEntity
            {
                ID = Guid.Parse("C5DE45D7-64A0-4E8D-AC7F-BF5CFDFB0EFC"),
                Start = new DateTime(2021, 04, 12, 8, 32, 41),
                End = new DateTime(2021, 04, 13, 15, 12, 49),
                Type = Types.backend,
                Description = "Projekt hotov za necele 2 dny.",
                ProjectID = project.ID,
                UserID = user.ID
            };
            TimeTrackerDbContextSUT.Activities.Add(activity);

            await TimeTrackerDbContextSUT.SaveChangesAsync();

            // Save changes to the database
            TimeTrackerDbContextSUT.SaveChanges();

            // Retrieve the activity from the database
            var db = await DbContextFactory.CreateDbContextAsync();
            var actualActivity = await db.Activities
                .Include(a => a.Project)
                .Include(a => a.User)
                .SingleAsync(a => a.ID == activity.ID);

            // Assert that the retrieved activity matches the original activity
            Assert.Equal(activity.ID, actualActivity.ID);
            Assert.Equal(activity.Start, actualActivity.Start);
            Assert.Equal(activity.End, actualActivity.End);
            Assert.Equal(activity.Type, actualActivity.Type);
            Assert.Equal(activity.Description, actualActivity.Description);
            Assert.NotNull(actualActivity.Project);
            Assert.Equal(activity.ProjectID, actualActivity.Project.ID);
            Assert.NotNull(actualActivity.User);
            Assert.Equal(activity.UserID, actualActivity.User.ID);
        }


        [Fact]
        public async Task NewActivityAdded2()
        {
            // Create a new project and add it to the context
            var project = new ProjectEntity
            {
                ID = Guid.Parse("00000000-4B3B-4A7A-A47F-58C7E3A3A2F7"),
                Name = "Test Project 1"
            };
            TimeTrackerDbContextSUT.Projects.Add(project);

            // Create a new user and add it to the context
            var user = new UserEntity
            {
                ID = Guid.Parse("11111111-998C-4736-9A8E-03A7C121BB9C"),
                Name = "Test User",
                LastName = "2222",
                Photo = "www.uztojede.cz"
            };
            TimeTrackerDbContextSUT.Users.Add(user);

            // Save changes to the context to persist the new project and user
            await TimeTrackerDbContextSUT.SaveChangesAsync();

            var activity = new ActivityEntity()
            {
                ID = Guid.Parse("7c6cdd3e-481d-427a-a971-ae306aba8c95"),
                Start = new DateTime(2023, 01, 01, 7, 01, 09),
                End = new DateTime(2023, 03, 29, 16, 01, 53),
                Type = Types.hiring,
                Description = "Podarilo se nam privest 3 nove kolegy.",
                ProjectID = project.ID,
                UserID = user.ID
            };
            TimeTrackerDbContextSUT.Activities.Add(activity);

            await TimeTrackerDbContextSUT.SaveChangesAsync();

            // Save changes to the database
            TimeTrackerDbContextSUT.SaveChanges();

            // Retrieve the activity from the database
            var db = await DbContextFactory.CreateDbContextAsync();
            var actualActivity = await db.Activities
                .Include(a => a.Project)
                .Include(a => a.User)
                .SingleAsync(a => a.ID == activity.ID);

            // Assert that the retrieved activity matches the original activity
            Assert.Equal(activity.ID, actualActivity.ID);
            Assert.Equal(activity.Start, actualActivity.Start);
            Assert.Equal(activity.End, actualActivity.End);
            Assert.Equal(activity.Type, actualActivity.Type);
            Assert.Equal(activity.Description, actualActivity.Description);
            Assert.NotNull(actualActivity.Project);
            Assert.Equal(activity.ProjectID, actualActivity.Project.ID);
            Assert.NotNull(actualActivity.User);
            Assert.Equal(activity.UserID, actualActivity.User.ID);
        }
    }
}
