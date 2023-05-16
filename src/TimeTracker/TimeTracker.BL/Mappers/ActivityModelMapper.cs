using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;
using System.Linq;
using System;

namespace TimeTracker.BL.Mappers;

public class ActivityModelMapper : ModelMapperBase<ActivityEntity, ActivityListModel, ActivityDetailModel>, IActivityModelMapper
{
    public override ActivityListModel MapToListModel(ActivityEntity? entity)
        => entity is null
            ? ActivityListModel.Empty
            : new ActivityListModel
            {
                ID = entity.ID,
                Start = entity.Start,
                End = entity.End,
                Type = entity.Type,
                ProjectID = entity.ProjectID,
                UserID = entity.UserID
            };

    public override ActivityDetailModel MapToDetailModel(ActivityEntity? entity)
        => entity is null
            ? ActivityDetailModel.Empty
            : new ActivityDetailModel
            {
                ID = entity.ID,
                Start = entity.Start,
                End = entity.End,
                Type = entity.Type,
                Description = entity.Description,
                UserID = entity.UserID,
                ProjectID = entity.ProjectID
            };

    public override ActivityEntity MapToEntity(ActivityDetailModel model)
        => new()
        {
            ID = model.ID,
            Start = model.Start,
            End = model.End,
            Type = model.Type,
            Description = model.Description,
            ProjectID = model.ProjectID,
            UserID = model.UserID
        };
}