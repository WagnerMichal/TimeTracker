using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Facades;

public interface IActivityFacade : IFacade<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    Task<IEnumerable<ActivityListModel>> GetAsyncByUserId(Guid UserId);
    Task<bool> ConflictedActivities(DateTime Start, DateTime End, Guid UserID);
}
