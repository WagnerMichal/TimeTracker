using TimeTracker.BL.Mappers;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;

using TimeTracker.DAL.Mappers;
using TimeTracker.DAL.UnitOfWork;

namespace TimeTracker.BL.Facades;

public class ProjectFacade :
    FacadeBase<ProjectEntity, ProjectListModel, ProjectDetailModel, ProjectEntityMapper>, IProjectFacade
{
    public ProjectFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IProjectModelMapper modelMapper)
        : base(unitOfWorkFactory, modelMapper)
    {
    }
}