using TimeTracker.Common.Tests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TimeTracker.DAL.Entities;
using Xunit.Abstractions;

namespace TimeTracker.DAL.Tests
{
    public class ProjectTests : DbContextTestsBase
    {

        public ProjectTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task Project_add_Added()
        {
            var project = new ProjectEntity()
            {
                ID = Guid.Parse("7c6cdd3e-481d-427a-a971-ae306aba8c95"),
                Name = "SimpleApp",
                Description = "Develop simple app"

            };

            TimeTrackerDbContextSUT.Projects.Add(project);
            TimeTrackerDbContextSUT.SaveChanges();

            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Projects
                .SingleAsync(i => i.ID == project.ID);
            DeepAssert.Equal(project, actualEntity);
        }
    }
}