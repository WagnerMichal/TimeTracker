using TimeTracker.Common.Tests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Mappers;
using TimeTracker.BL.Models;
using TimeTracker.Common.Enums;
using TimeTracker.DAL.Seeds;
using TimeTracker.Common.Tests.Seeds;
using TimeTracker.DAL.UnitOfWork;
using Xunit;
using Xunit.Abstractions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TimeTracker.BL.Tests
{
    public class ActivityFacadeTests : FacadeTestsBase
    {
        private readonly IActivityFacade _activityFacadeSUT;
        private readonly IUserFacade _userFacadeSUT;
        private readonly IProjectFacade _projectFacadeSUT;

        private ActivityDetailModel _createdActivity;
        private ActivityDetailModel _createdActivity1;
        private ActivityDetailModel _createdActivity2;
        private UserDetailModel _createdUser1;
        private ProjectDetailModel _createdProject;


        public ActivityFacadeTests(ITestOutputHelper output) : base(output)
        {
            _activityFacadeSUT = new ActivityFacade(UnitOfWorkFactory, ActivityModelMapper);
            _userFacadeSUT = new UserFacade(UnitOfWorkFactory, UserModelMapper);
            _projectFacadeSUT = new ProjectFacade(UnitOfWorkFactory, ProjectModelMapper);

            // Prommena pro datum 12.4.2021 a cas 8:32:41
            var date1 = new DateTime(2021, 04, 12, 8, 32, 41);

            // Prommena pro datum 13.4.2021 a cas 15:12:49
            var date2 = new DateTime(2021, 04, 13, 15, 12, 49);

            _createdUser1 = new UserDetailModel()
            {
                ID = Guid.Parse("00000000-0000-0000-0000-111111111111"),
                Name = "Adam",
                LastName = "Wagner",
                Photo = "www.google.com"
            };

            _createdProject = new ProjectDetailModel()
            {
                ID = Guid.Parse("00000000-0000-1111-0000-111111111111"),
                Name = "Project 1"
            };

            _createdActivity = new ActivityDetailModel()
            {
                ID = Guid.Parse("11111111-0000-1111-0000-111111111111"),
                Start = date1,
                End = date2,
                Type = Types.marketing,
                Description = @"popis",
                ProjectID = _createdProject.ID,
                UserID = _createdUser1.ID
            };

            _createdActivity1 = new ActivityDetailModel()
            {
                ID = Guid.NewGuid(),
                Start = new DateTime(2021, 04, 12, 8, 32, 41),
                End = new DateTime(2021, 04, 13, 15, 12, 49),
                Type = Types.marketing,
                Description = @"popis1",
                ProjectID = _createdProject.ID,
                UserID = _createdUser1.ID
            };

            _createdActivity2 = new ActivityDetailModel()
            {
                ID = Guid.NewGuid(),
                Start = new DateTime(2021, 04, 12, 8, 32, 41),
                End = new DateTime(2021, 04, 13, 15, 12, 49),
                Type = Types.marketing,
                Description = @"popis2",
                ProjectID = _createdProject.ID,
                UserID = _createdUser1.ID
            };
        }

        [Fact]
        public async Task CreateActivity()
        {
            var user = new UserDetailModel()
            {
                ID = Guid.NewGuid(),
                Name = "Adam",
                LastName = "Wagner",
                Photo = "www.google.com"
            };

            var project = new ProjectDetailModel()
            {
                ID = Guid.NewGuid(),
                Name = "Project 1"
            };

            user = await _userFacadeSUT.SaveAsync(user);
            project = await _projectFacadeSUT.SaveAsync(project);

            var activity = new ActivityDetailModel()
            {
                ID = Guid.NewGuid(),
                Start = new DateTime(2021, 04, 12, 8, 32, 41),
                End = new DateTime(2021, 04, 16, 8, 32, 41),
                Type = Types.marketing,
                Description = @"popis",
                ProjectID = project.ID,
                UserID = user.ID
            };

            var id = await _activityFacadeSUT.SaveAsync(activity);

            Assert.NotNull(id);
        }

        [Fact]
        public async Task GetAllActivities()
        {
            var user = new UserDetailModel()
            {
                ID = Guid.NewGuid(),
                Name = "Adam",
                LastName = "Wagner",
                Photo = "www.google.com"
            };

            var project = new ProjectDetailModel()
            {
                ID = Guid.NewGuid(),
                Name = "Project 1"
            };

            user = await _userFacadeSUT.SaveAsync(user);
            project = await _projectFacadeSUT.SaveAsync(project);

            var activity = new ActivityDetailModel()
            {
                ID = Guid.NewGuid(),
                Start = new DateTime(2021, 04, 12, 8, 32, 41),
                End = new DateTime(2021, 04, 16, 8, 32, 41),
                Type = Types.marketing,
                Description = @"popis",
                ProjectID = project.ID,
                UserID = user.ID
            };

            await _activityFacadeSUT.SaveAsync(activity);

            var activities = await _activityFacadeSUT.GetAsync();

            Assert.NotEmpty(activities);
        }

        [Fact]
        public async Task GetActivityById()
        {
            var activity = await _activityFacadeSUT.GetAsync(ActivitySeedsCommon.Work.ID);

            DeepAssert.Equal(ActivityModelMapper.MapToDetailModel(ActivitySeedsCommon.Work), activity);
        }

        [Fact]
        public async Task UpdateActivity()
        {
            //Arrange
            var detailModel = ActivityModelMapper.MapToDetailModel(ActivitySeedsCommon.Activity1);
            detailModel.Description = "Changed recipe name 1";

            //Act
            await _activityFacadeSUT.SaveAsync(detailModel);

            //Assert
            var returnedModel = await _activityFacadeSUT.GetAsync(detailModel.ID);
            DeepAssert.Equal(detailModel, returnedModel);
        }

        [Fact]
        public async Task DeleteActivity()
        {
            var user = new UserDetailModel()
            {
                ID = Guid.NewGuid(),
                Name = "Adam",
                LastName = "Wagner",
                Photo = "www.google.com"
            };

            var project = new ProjectDetailModel()
            {
                ID = Guid.NewGuid(),
                Name = "Project 1"
            };

            user = await _userFacadeSUT.SaveAsync(user);
            project = await _projectFacadeSUT.SaveAsync(project);

            var activity = new ActivityDetailModel()
            {
                ID = Guid.NewGuid(),
                Start = new DateTime(2021, 04, 12, 8, 32, 41),
                End = new DateTime(2021, 04, 16, 8, 32, 41),
                Type = Types.marketing,
                Description = @"popis",
                ProjectID = project.ID,
                UserID = user.ID
            };

            var createdActivity = await _activityFacadeSUT.SaveAsync(activity);

            await using var help = await DbContextFactory.CreateDbContextAsync();

            Assert.False(await help.Activities.AnyAsync(i => i.ID == activity.ID));
        }

        [Fact]
        public async Task CreateConflictinActivities()
        {
            var user = new UserDetailModel()
            {
                ID = Guid.Parse("11111111-481d-427a-a971-ae306aba8c95"),
                Name = "Adam",
                LastName = "Wagner",
                Photo = "www.google.com"
            };

            var project = new ProjectDetailModel()
            {
                ID = Guid.Parse("7c6cdd3e-481d-427a-a971-ae306aba8c95"),
                Name = "Test Project"
            };

            // Save the user and project records
            user = await _userFacadeSUT.SaveAsync(user);
            project = await _projectFacadeSUT.SaveAsync(project);

            var activity1 = new ActivityDetailModel()
            {
                ID = Guid.Parse("01010101-481d-427a-a971-ae306aba8c95"),
                Start = new DateTime(2021, 04, 12, 8, 32, 41),
                End = new DateTime(2021, 04, 13, 15, 12, 49),
                Type = Types.marketing,
                Description = @"popis1",
                ProjectID = project.ID,
                UserID = user.ID
            };

            var activity2 = new ActivityDetailModel()
            {
                ID = Guid.NewGuid(),
                Start = new DateTime(2021, 04, 12, 8, 32, 41),
                End = new DateTime(2021, 04, 13, 15, 12, 49),
                Type = Types.marketing,
                Description = @"popis2",
                ProjectID = project.ID,
                UserID = user.ID
            };

            // Save the activities
            await _activityFacadeSUT.SaveAsync(activity1);
            await Assert.ThrowsAsync<Exception>(async () => await _activityFacadeSUT.SaveAsync(activity2));
        }

        [Fact]
        public async Task CreateMoreActivities()
        {
            var user = new UserDetailModel()
            {
                ID = Guid.Parse("11111111-481d-427a-a971-ae306aba8c95"),
                Name = "Adam",
                LastName = "Wagner",
                Photo = "www.google.com"
            };

            var project = new ProjectDetailModel()
            {
                ID = Guid.Parse("7c6cdd3e-481d-427a-a971-ae306aba8c95"),
                Name = "Test Project"
            };

            // Save the user and project records
            user = await _userFacadeSUT.SaveAsync(user);
            project = await _projectFacadeSUT.SaveAsync(project);

            var activity1 = new ActivityDetailModel()
            {
                ID = Guid.Parse("01010101-481d-427a-a971-ae306aba8c95"),
                Start = new DateTime(2021, 04, 12, 8, 32, 41),
                End = new DateTime(2021, 04, 13, 15, 12, 49),
                Type = Types.marketing,
                Description = @"popis1",
                ProjectID = project.ID,
                UserID = user.ID
            };

            var activity2 = new ActivityDetailModel()
            {
                ID = Guid.NewGuid(),
                Start = new DateTime(2021, 04, 16, 8, 32, 41),
                End = new DateTime(2021, 04, 17, 15, 12, 49),
                Type = Types.marketing,
                Description = @"popis2",
                ProjectID = project.ID,
                UserID = user.ID
            };

            // Save the activities
            await _activityFacadeSUT.SaveAsync(activity1);
            await _activityFacadeSUT.SaveAsync(activity2);
        }
    }
}