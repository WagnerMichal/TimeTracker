using TimeTracker.DAL.Entities;
namespace TimeTracker.DAL.Mappers;

public class UserInProjectEntityMapper : IEntityMapper<UserInProjectEntity>
{
    public void MapToExistingEntity(UserInProjectEntity existingEntity, UserInProjectEntity newEntity)
    {
        existingEntity.UserID = newEntity.UserID;
        existingEntity.ProjectID = newEntity.ProjectID;
    }
}