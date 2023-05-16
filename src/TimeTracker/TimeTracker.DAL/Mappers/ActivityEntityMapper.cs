
using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Mappers;

public class ActivityEntityMapper : IEntityMapper<ActivityEntity>
{
    public void MapToExistingEntity(ActivityEntity existingEntity, ActivityEntity newEntity)
    {
        existingEntity.Start = newEntity.Start;
        existingEntity.End = newEntity.End;
        existingEntity.Type = newEntity.Type;
        existingEntity.Description = newEntity.Description;
        existingEntity.UserID = newEntity.UserID;
        existingEntity.ProjectID = newEntity.ProjectID;
    }
}