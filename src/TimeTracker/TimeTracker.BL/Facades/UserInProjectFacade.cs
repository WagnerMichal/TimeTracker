using Microsoft.EntityFrameworkCore;
using TimeTracker.BL.Mappers;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Mappers;
using TimeTracker.DAL.UnitOfWork;
using TimeTracker.DAL.Repositories;


namespace TimeTracker.BL.Facades;

public class UserInProjectFacade :
    FacadeBase<UserInProjectEntity, UserInProjectListModel, UserInProjectDetailModel, UserInProjectEntityMapper>, IUserInProjectFacade


{
    private readonly IUserInProjectModelMapper _userInProjectModelMapper;

    public UserInProjectFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserInProjectModelMapper userInProjectModelMapper)
        : base(unitOfWorkFactory, userInProjectModelMapper) =>
        _userInProjectModelMapper = userInProjectModelMapper;

    public async Task SaveAsync(UserInProjectListModel model, Guid projectID)
    {
        UserInProjectEntity entity = _userInProjectModelMapper.MapToEntity(model, projectID);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<UserInProjectEntity> repository =
            uow.GetRepository<UserInProjectEntity, UserInProjectEntityMapper>();

        if (await repository.ExistsAsync(entity))
        {
            await repository.UpdateAsync(entity);
            await uow.CommitAsync();
        }
    }

    public async Task<IEnumerable<UserInProjectListModel>> GetAsyncByUserId(Guid UserId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        List<UserInProjectEntity> entities = await uow
            .GetRepository<UserInProjectEntity, UserInProjectEntityMapper>()
            .Get()
            .Include(a => a.User)
            .Include(a => a.Project)
            .Where(a => a.UserID == UserId)
            .ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }

    public async Task<IEnumerable<UserInProjectListModel>> GetAsyncByProjectId(Guid ProjectId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        List<UserInProjectEntity> entities = await uow
            .GetRepository<UserInProjectEntity, UserInProjectEntityMapper>()
            .Get()
            .Include(u => u.User)
            .Include(u => u.Project)
            .Where(a => a.ProjectID == ProjectId)
            .ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }

    public async Task<UserInProjectDetailModel> SaveAsync(UserInProjectDetailModel model, Guid userInProjectID)
    {
        UserInProjectEntity entity = _userInProjectModelMapper.MapToEntity(model);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<UserInProjectEntity> repository =
            uow.GetRepository<UserInProjectEntity, UserInProjectEntityMapper>();

        await repository.InsertAsync(entity);
        await uow.CommitAsync();

        return ModelMapper.MapToDetailModel(entity);
    }
}
