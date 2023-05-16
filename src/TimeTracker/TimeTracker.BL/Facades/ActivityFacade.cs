using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using TimeTracker.BL.Mappers;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;

using TimeTracker.DAL.Mappers;
using TimeTracker.DAL.UnitOfWork;

namespace TimeTracker.BL.Facades;

public class ActivityFacade:
    FacadeBase<ActivityEntity, ActivityListModel, ActivityDetailModel, ActivityEntityMapper>, IActivityFacade

{
    public ActivityFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IActivityModelMapper modelMapper)
        : base(unitOfWorkFactory, modelMapper)
    {
    }
    public async override Task<ActivityDetailModel> SaveAsync(ActivityDetailModel model)
    {
        bool result = await ConflictedActivities(model.Start, model.End, model.UserID);
        if (result)
        {
            throw new Exception("Conflicted activities.");
        }
        return await base.SaveAsync(model);
        
    }
    
    public async Task<IEnumerable<ActivityListModel>> GetAsyncByUserId(Guid UserId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        List<ActivityEntity> entities = await uow
            .GetRepository<ActivityEntity, ActivityEntityMapper>()
            .Get()
            .Where(a => a.UserID == UserId)
            .ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }

    public async Task<bool> ConflictedActivities(DateTime Start, DateTime End, Guid UserID)
    {
        var uow = UnitOfWorkFactory.Create();
        var dbSetActivities = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();

        // Find the last activity for the specified user
        var lastActivity = await dbSetActivities
            .Where(x => x.UserID == UserID&& x.End <= Start)
            .OrderByDescending(x => x.End)
            .FirstOrDefaultAsync();

        // Check for conflicting activities
        if (lastActivity == null)
        {
            bool conflictTime = await dbSetActivities
            .AnyAsync(x => x.UserID == UserID // Exclude lastActivity from conflicting activities check
                && (
                    (Start <= x.End && x.End <= End) ||
                    (Start <= x.Start && x.Start <= End) ||
                    (x.Start <= End && End <= x.End) ||
                    (x.Start <= Start && Start <= x.End)
                )
            );
            return conflictTime;
        }
        else
        {
            bool conflictTime = await dbSetActivities
            .AnyAsync(x => x.UserID == UserID
                && x == lastActivity // Exclude lastActivity from conflicting activities check
                && (
                    (Start <= x.End && x.End <= End) ||
                    (Start <= x.Start && x.Start <= End) ||
                    (x.Start <= End && End <= x.End) ||
                    (x.Start <= Start && Start <= x.End)
                )
            );
            return conflictTime;
        }
    }

    public async Task<IEnumerable<ActivityListModel>> GetFilteredActivities(Guid? userID, DateTime? start, DateTime? end, string filteredBy)
    {
        if (userID == null)
        {
            return new List<ActivityListModel>();
        }

        var now = DateTime.Now;
        await using var uow = UnitOfWorkFactory.Create();

        var activities = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get()
            .Where(x => x.UserID == userID.Value);

        switch (filteredBy)
        {
            case "thisYear":
                activities = activities.Where(x => x.Start.Year == now.Year);
                break;
            case "thisMonth":
                activities = activities.Where(x => x.Start.Year == now.Year && x.Start.Month == now.Month);
                break;
            case "lastMonth":
                var lastMonth = now.AddMonths(-1);
                activities = activities.Where(x => x.Start.Year == lastMonth.Year && x.Start.Month == lastMonth.Month);
                break;
            case "lastYear":
                activities = activities.Where(x => x.Start.Year == now.Year - 1);
                break;
            default:
                if (start != null)
                {
                    activities = activities.Where(x => x.Start >= start.Value);
                }
                if (end != null)
                {
                    activities = activities.Where(x => x.Start <= end.Value);
                }
                break;
        }

        var activityModels = activities.Select(x => new ActivityListModel
        {
            UserID = x.UserID,
            ProjectID = x.ProjectID,
            Start = x.Start,
            End = x.End,
            Type = x.Type

        });

        return activityModels;
    }
}


