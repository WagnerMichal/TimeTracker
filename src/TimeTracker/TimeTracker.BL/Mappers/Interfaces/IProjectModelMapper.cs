using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Mappers;

public interface IProjectModelMapper : IModelMapper<ProjectEntity, ProjectListModel, ProjectDetailModel>
{
}
