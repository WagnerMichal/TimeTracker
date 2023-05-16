using TimeTracker.BL.Models;

using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Facades;

public interface IProjectFacade : IFacade<ProjectEntity, ProjectListModel, ProjectDetailModel>
{
}
