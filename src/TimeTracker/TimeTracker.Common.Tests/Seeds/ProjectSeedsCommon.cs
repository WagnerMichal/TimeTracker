using TimeTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace TimeTracker.Common.Tests.Seeds;

public static class ProjectSeedsCommon
{
    public static readonly ProjectEntity EmptyProjectEntity = new()
    {
        ID = default,
        Name = default!,
        Description = default,
    };

    public static readonly ProjectEntity ProjectEntity = new()
    {
        ID = Guid.Parse(input: "fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
        Name = "TimeTracker",
        Description = "Random project descp",
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly ProjectEntity ProjectEntityWithNoActivities = ProjectEntity with { ID = Guid.Parse("98B7F7B6-0F51-43B3-B8C0-B5FCFFF6DC2E"), Activities = Array.Empty<ActivityEntity>(), Users = Array.Empty<UserInProjectEntity>() };
    public static readonly ProjectEntity ProjectEntityUpdate = ProjectEntity with { ID = Guid.Parse("0953F3CE-7B1A-48C1-9796-D2BAC7F67868"), Activities = Array.Empty<ActivityEntity>(), Users = Array.Empty<UserInProjectEntity>() };
    public static readonly ProjectEntity ProjectEntityDelete = ProjectEntity with { ID = Guid.Parse("5DCA4CEA-B8A8-4C86-A0B3-FFB78FBA1A09"), Activities = Array.Empty<ActivityEntity>(), Users = Array.Empty<UserInProjectEntity>() };

    public static readonly ProjectEntity ProjectForUserInProjectEntityUpdate = ProjectEntity with { ID = Guid.Parse("4FD824C0-A7D1-48BA-8E7C-4F136CF8BF31"), Users = Array.Empty<UserInProjectEntity>() };
    public static readonly ProjectEntity ProjectForUserInProjectEntityDelete = ProjectEntity with { ID = Guid.Parse("F78ED923-E094-4016-9045-3F5BB7F2EB88"), Users = new List<UserInProjectEntity>() };
    static ProjectSeedsCommon()
    {
        ProjectEntity.Users.Add(UserInProjectSeedsCommon.UserInProjectEntity1);
        ProjectEntity.Users.Add(UserInProjectSeedsCommon.UserInProjectEntity2);

        ProjectForUserInProjectEntityDelete.Users.Add(UserInProjectSeedsCommon.UserInProjectEntityDelete);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>().HasData(
            ProjectEntity with { Users = Array.Empty<UserInProjectEntity>() },
            ProjectEntityWithNoActivities,
            ProjectEntityUpdate,
            ProjectEntityDelete,
            ProjectForUserInProjectEntityUpdate,
            ProjectForUserInProjectEntityDelete with { Users = Array.Empty<UserInProjectEntity>() }
        );
    }
}