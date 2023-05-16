using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Facades;

public interface IUserFacade : IFacade<UserEntity, UserListModel, UserDetailModel>
{
}
