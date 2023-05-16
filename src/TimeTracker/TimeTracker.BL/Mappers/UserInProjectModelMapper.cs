using System;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Mappers;

public class UserInProjectModelMapper :
    ModelMapperBase<UserInProjectEntity, UserInProjectListModel, UserInProjectDetailModel>, IUserInProjectModelMapper
{
    public override UserInProjectListModel MapToListModel(UserInProjectEntity? entity)
        => entity?.User is null
            ? UserInProjectListModel.Empty
            : new UserInProjectListModel
            {
                ID = entity.ID,
                ProjectID = entity.Project.ID,
                ProjectName = entity.Project.Name,
                UserID = entity.User.ID,
                UserName = entity.User.Name,
                UserLastName = entity.User.LastName,
                UserPhoto = entity.User.Photo
            };

    public override UserInProjectDetailModel MapToDetailModel(UserInProjectEntity? entity)
        => entity?.User is null
            ? UserInProjectDetailModel.Empty
            : new UserInProjectDetailModel
            {
                ID = entity.ID,
                ProjectID = entity.Project.ID,
                ProjectName = entity.Project.Name,
                UserID = entity.User.ID,
                UserName = entity.User.Name,
                UserLastName = entity.User.LastName,
                UserEmail = entity.User.Email,
                UserPhoto = entity.User.Photo

            };

    public UserInProjectListModel MapToListModel(UserInProjectDetailModel detailModel)
        => new()
        {
            ID = detailModel.ID,
            ProjectID = detailModel.ProjectID,
            ProjectName = detailModel.ProjectName,
            UserID = detailModel.UserID,
            UserName = detailModel.UserName,
            UserLastName = detailModel.UserLastName,
            UserPhoto = detailModel.UserPhoto
        };

    public void MapToExistingDetailModel(UserInProjectDetailModel existingDetailModel, UserListModel user, ProjectDetailModel project)
    {
        existingDetailModel.ProjectID = project.ID;
        existingDetailModel.ProjectName = project.Name;
        existingDetailModel.UserID = user.ID;
        existingDetailModel.UserName = user.Name;
        existingDetailModel.UserLastName = user.LastName;
        existingDetailModel.UserPhoto = user.Photo;
    }

    public override UserInProjectEntity MapToEntity(UserInProjectDetailModel model)
        => new()
        {
            ID = model.ID,
            UserID = model.UserID,
            ProjectID = model.ProjectID
        };

    public UserInProjectEntity MapToEntity(UserInProjectDetailModel model, Guid ProjectID)
        => new()
        {
            ID = model.ID,
            UserID = model.UserID,
            ProjectID = ProjectID
        };

    public UserInProjectEntity MapToEntity(UserInProjectListModel model, Guid ProjectID)
        => new()
        {
            ID = model.ID,
            UserID = model.UserID,
            ProjectID = ProjectID
        };
}