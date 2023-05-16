using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;
using Xunit.Abstractions;

namespace TimeTracker.BL.Tests
{
    public class ProjectFacadeTests : FacadeTestsBase
    {
        private readonly IProjectFacade _projectFacadeSUT;

        private ProjectDetailModel _createdProject;

        public ProjectFacadeTests(ITestOutputHelper output) : base(output)
        {
            _projectFacadeSUT = new ProjectFacade(UnitOfWorkFactory, ProjectModelMapper);

            _createdProject = new ProjectDetailModel()
            {
                ID = Guid.NewGuid(),
                Name = "Project 1",
            };
        }

        [Fact]
        public async Task CreateProject()
        {
            _createdProject = await _projectFacadeSUT.SaveAsync(_createdProject);

            var fromDbProject = await _projectFacadeSUT.GetAsync(_createdProject.ID);

            Assert.NotNull(fromDbProject);
            Assert.Equal(fromDbProject.ID, _createdProject.ID);
        }

        [Fact]
        public async Task GetAllProjects()
        {
            _createdProject = await _projectFacadeSUT.SaveAsync(_createdProject);

            var projects = await _projectFacadeSUT.GetAsync();

            Assert.NotEmpty(projects);
        }

        [Fact]
        public async Task GetProjectById()
        {
            _createdProject = await _projectFacadeSUT.SaveAsync(_createdProject);

            var project = await _projectFacadeSUT.GetAsync(_createdProject.ID);

            Assert.NotNull(project);
            Assert.Equal(project.ID, _createdProject.ID);
        }

        [Fact]
        public async Task UpdateProject()
        {
            _createdProject = await _projectFacadeSUT.SaveAsync(_createdProject);

            var fromDbProject = await _projectFacadeSUT.GetAsync(_createdProject.ID);

            if(fromDbProject != null)
            {
                fromDbProject.Name = "Project edited";

                fromDbProject = await _projectFacadeSUT.SaveAsync(fromDbProject);

                Assert.NotEqual(fromDbProject.Name, _createdProject.Name);
                Assert.Equal("Project 1", _createdProject.Name);
                Assert.Equal("Project edited", fromDbProject.Name);
                Assert.Equal(_createdProject.ID, fromDbProject.ID);
            }
            else
            {
                throw new Exception("Returned null");
            }
        }

        [Fact]
        public async Task DeleteProject()
        {
            _createdProject = await _projectFacadeSUT.SaveAsync(_createdProject);

            await _projectFacadeSUT.DeleteAsync(_createdProject.ID);

            var fromDbProject = await _projectFacadeSUT.GetAsync(_createdProject.ID);

            Assert.Null(fromDbProject);
        }
    }
}
