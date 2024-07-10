using InternSystem.Application.Common.Persistences.IRepositories.IBaseRepositories;
using InternSystem.Domain.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;
namespace InternSystem.Infrastructure.Persistences.Repositories.BaseRepositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
{
    private readonly DbContext _dbContext;

    public IQueryable<T> Entities => _dbContext.Set<T>().Where(e => e.IsActive && !e.IsDelete);

    public BaseRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Entities.ToListAsync();
    }

    public IQueryable<T> GetAllQueryable()
    {
        return Entities;
    }

    public Task<IQueryable<T>> GetAllIQueryableAsync()
    {
        return Task.FromResult(Entities);
    }

    public async Task<T> GetByIdAsync(object id)
    {
        var entity = await _dbContext.Set<T>().FindAsync(id);
        if (entity != null && entity.IsActive && !entity.IsDelete)
        {
            return (entity);
        }
        return null;
    }

    public async Task<T> GetTruongHocsByTenAsync(object name)
    {
        return await _dbContext.Set<T>().FirstOrDefaultAsync(e => EF.Property<string>(e, "Name") == name);
    }

    public async Task<T> AddAsync(T entity)
    {
        if (_dbContext.Entry(entity).State == EntityState.Detached)
        {
            _dbContext.Set<T>().Attach(entity);
        }
        await _dbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public void Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }

    public void Remove(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }
}