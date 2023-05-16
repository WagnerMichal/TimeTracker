using TimeTracker.BL.Models;

using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Facades;

public interface IUserInProjectFacade
{
    Task<UserInProjectDetailModel> SaveAsync(UserInProjectDetailModel model, Guid projectID);
    Task SaveAsync(UserInProjectListModel model, Guid projectID);
    Task DeleteAsync(Guid ID);

    Task<IEnumerable<UserInProjectListModel>> GetAsyncByUserId(Guid UserId);
    
    Task<IEnumerable<UserInProjectListModel>> GetAsyncByProjectId(Guid ProjectId);
}
