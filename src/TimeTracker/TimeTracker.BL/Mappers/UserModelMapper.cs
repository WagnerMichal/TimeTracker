using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Mappers;

public class UserModelMapper : ModelMapperBase<UserEntity, UserListModel, UserDetailModel>, IUserModelMapper
{
    private readonly IUserInProjectModelMapper _userInProjectModelMapper;
    private readonly IActivityModelMapper _activityModelMapper;

    public UserModelMapper(IUserInProjectModelMapper userInProjectModelMapper, IActivityModelMapper activityModelMapper)
    {
        _userInProjectModelMapper = userInProjectModelMapper;
        _activityModelMapper = activityModelMapper;
    }

    public override UserListModel MapToListModel(UserEntity? entity)
        => entity is null
            ? UserListModel.Empty
            : new UserListModel
            {
                ID = entity.ID,
                Name = entity.Name,
                Email = entity.Email,
                LastName = entity.LastName,
                Photo = entity.Photo,
            };

    public override UserDetailModel MapToDetailModel(UserEntity? entity)
        => entity is null
            ? UserDetailModel.Empty
            : new UserDetailModel
            {
                ID = entity.ID,
                Name = entity.Name,
                LastName = entity.LastName,
                Photo = entity.Photo,
                Email = entity.Email,
                Projects = _userInProjectModelMapper.MapToListModel(entity.Projects).ToObservableCollection(),
                Activities = _activityModelMapper.MapToListModel(entity.Activities).ToObservableCollection()
            };

    public override UserEntity MapToEntity(UserDetailModel model)
        => new()
        {
            ID = model.ID,
            Name = model.Name,
            Email = model.Email,
            LastName = model.LastName,
            Photo = model.Photo
        };
}