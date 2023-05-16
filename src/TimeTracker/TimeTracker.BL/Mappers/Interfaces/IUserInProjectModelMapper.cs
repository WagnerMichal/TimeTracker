using System;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;


namespace TimeTracker.BL.Mappers;

public interface IUserInProjectModelMapper : IModelMapper<UserInProjectEntity, UserInProjectListModel, UserInProjectDetailModel>
{
    UserInProjectListModel MapToListModel(UserInProjectDetailModel detailModel);
    UserInProjectEntity MapToEntity(UserInProjectListModel model, Guid projectID);
    UserInProjectEntity MapToEntity(UserInProjectDetailModel model, Guid projectID);
    void MapToExistingDetailModel(UserInProjectDetailModel existingDetailModel, UserListModel User, ProjectDetailModel Project);
}
