using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Facades;

public interface IFacade<TEntity, TListModel, TDetailModel>
    where TEntity: class, IEntity
    where TListModel: IModel
    where TDetailModel: class, IModel

{
    Task DeleteAsync(Guid ID);
    Task<TDetailModel?> GetAsync(Guid ID);
    Task<IEnumerable<TListModel>> GetAsync();
    Task<TDetailModel> SaveAsync(TDetailModel model);
}
