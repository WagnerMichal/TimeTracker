using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Models;

using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Mappers;

public class ProjectModelMapper : ModelMapperBase<ProjectEntity, ProjectListModel, ProjectDetailModel>, IProjectModelMapper
{
    private readonly IUserInProjectModelMapper _userInProjectModelMapper;

    public ProjectModelMapper(IUserInProjectModelMapper userInProjectModelMapper) => _userInProjectModelMapper = userInProjectModelMapper;

    public override ProjectListModel MapToListModel(ProjectEntity? entity)
        => entity is null
            ? ProjectListModel.Empty
            : new ProjectListModel
            {
                ID = entity.ID,
                Name = entity.Name,
            };

    public override ProjectDetailModel MapToDetailModel(ProjectEntity? entity)
        => entity is null
            ? ProjectDetailModel.Empty
            : new ProjectDetailModel
            {
                ID = entity.ID,
                Name = entity.Name,
                Description = entity.Description,
                Users = _userInProjectModelMapper.MapToListModel(entity.Users).ToObservableCollection()
            };

    public override ProjectEntity MapToEntity(ProjectDetailModel model)
        => new()
        {
            ID = model.ID,
            Name = model.Name,
            Description = model.Description,
        };
}