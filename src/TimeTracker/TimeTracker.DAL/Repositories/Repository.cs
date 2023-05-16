using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Mappers;
using System.Collections;

namespace TimeTracker.DAL.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly IEntityMapper<TEntity> _entityMapper;

    public Repository(DbContext dbContext, IEntityMapper<TEntity> entityMapper)
    {
        _dbSet = dbContext.Set<TEntity>();
        _entityMapper = entityMapper;
    }

    public IQueryable<TEntity> Get() => _dbSet;

    public async ValueTask<bool> ExistsAsync(TEntity entity)
        => entity.ID != Guid.Empty && await _dbSet.AnyAsync(e => e.ID == entity.ID);

    public async Task<TEntity> InsertAsync(TEntity entity)
        => (await _dbSet.AddAsync(entity)).Entity;
    
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        TEntity existingEntity = await _dbSet.SingleAsync(e => e.ID == entity.ID);
        _entityMapper.MapToExistingEntity(existingEntity, entity);
        return existingEntity;
    }

    public void Delete(Guid entityID) => _dbSet.Remove(_dbSet.Single(i => i.ID == entityID));
}