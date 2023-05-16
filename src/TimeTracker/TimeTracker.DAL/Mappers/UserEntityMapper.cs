using TimeTracker.DAL.Entities;
namespace TimeTracker.DAL.Mappers;

public class UserEntityMapper : IEntityMapper<UserEntity>
{
    public void MapToExistingEntity(UserEntity existingEntity, UserEntity newEntity)
    {
        existingEntity.Name = newEntity.Name;
        existingEntity.LastName = newEntity.LastName;
        existingEntity.Photo = newEntity.Photo;
        existingEntity.Email = newEntity.Email;
    }
}