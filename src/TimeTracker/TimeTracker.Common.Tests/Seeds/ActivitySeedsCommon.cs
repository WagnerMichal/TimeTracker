using TimeTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Common.Enums;

namespace TimeTracker.Common.Tests.Seeds;

public static class ActivitySeedsCommon
{
    public static readonly ActivityEntity EmptyActivityEntity = new()
    {
        ID = default,
        UserID = default,
        ProjectID = default,
        Start = default!,
        Description = default!,
        End = default!,
        Type = default,
        Project = default,
        User = default,
    };

    public static readonly ActivityEntity Work = new()
    {
        ID = Guid.Parse(input: "06a8a2cf-ea03-4095-a3e4-aa0291fe9c75"),
        UserID = UserSeedsCommon.UserEntity.ID,
        ProjectID = ProjectSeedsCommon.ProjectEntity.ID,
        Start = new DateTime(2010, 04, 12, 8, 32, 41),
        Description = "fsdfsdfsdfsdfsdf",
        End = new DateTime(2010, 04, 13, 20, 32, 41),
        Type = Types.frontend,
        Project = ProjectSeedsCommon.ProjectEntity,
        User = UserSeedsCommon.UserEntity,
    };


    public static readonly ActivityEntity Activity1 = new()
    {
        ID = Guid.Parse(input: "06a8a2cf-1111-4095-0000-aa0291fe9c75"),
        UserID = UserSeedsCommon.UserEntity.ID,
        ProjectID = ProjectSeedsCommon.ProjectEntity.ID,
        Start = new DateTime(2020, 04, 12, 8, 32, 41),
        Description = "fsdfsdfsdfsdfsdf",
        End = new DateTime(2020, 04, 13, 20, 32, 41),
        Type = Types.frontend,
        Project = ProjectSeedsCommon.ProjectEntity,
        User = UserSeedsCommon.UserEntity,
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityEntity>().HasData(
            Work with { Project = null, User = null},
            Activity1 with { Project = null, User = null}
        );
    }
}