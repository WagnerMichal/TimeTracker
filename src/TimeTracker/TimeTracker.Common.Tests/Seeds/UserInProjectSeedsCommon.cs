using TimeTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Common.Tests.Seeds;

namespace TimeTracker.Common.Tests.Seeds;

public static class UserInProjectSeedsCommon
{
    public static readonly UserInProjectEntity EmptyUserInProjectEntity = new()
    {
        ID = default,
        UserID = default,
        ProjectID = default,
        Project = default,
        User = default,
    };
    public static readonly UserInProjectEntity UserInProjectEntity1 = new()
    {
        ID = Guid.Parse(input: "0d4fa150-ad80-4d46-a511-4c666166ec5e"),
        UserID = UserSeedsCommon.UserEntity1.ID,
        ProjectID = ProjectSeedsCommon.ProjectEntity.ID,
        Project = ProjectSeedsCommon.ProjectEntity,
        User = UserSeedsCommon.UserEntity,
    };

    public static readonly UserInProjectEntity UserInProjectEntity2 = new()
    {
        ID = Guid.Parse(input: "87833e66-05ba-4d6b-900b-fe5ace88dbd8"),
        UserID = UserSeedsCommon.UserEntity2.ID,
        ProjectID = ProjectSeedsCommon.ProjectEntity.ID,
        Project = ProjectSeedsCommon.ProjectEntity,
        User = UserSeedsCommon.UserEntity,
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly UserInProjectEntity UserInProjectEntityUpdate = UserInProjectEntity1 with { ID = Guid.Parse("A2E6849D-A158-4436-980C-7FC26B60C674"), Project = null, User = null, ProjectID = ProjectSeedsCommon.ProjectForUserInProjectEntityUpdate.ID };
    public static readonly UserInProjectEntity UserInProjectEntityDelete = UserInProjectEntity1 with { ID = Guid.Parse("30872EFF-CED4-4F2B-89DB-0EE83A74D279"), Project = null, User = null, ProjectID = ProjectSeedsCommon.ProjectForUserInProjectEntityDelete.ID };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserInProjectEntity>().HasData(
            UserInProjectEntity1 with { Project = null, User = null },
            UserInProjectEntity2 with { Project = null, User = null },
            UserInProjectEntityUpdate,
            UserInProjectEntityDelete
        );
    }
}