namespace InternSystem.Application.Common.Persistences.IRepositories.IBaseRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Entities { get; }
        //Task<PaginatedList<T>> GetPagging(IQueryable<T> query, int index, int pageSize);
        Task<T> GetTruongHocsByTenAsync(object name);
        Task<T> AddAsync(T entity);
        void Remove(T entity);
        void Update(T entity);
        IQueryable<T> GetAllQueryable();
        Task<IQueryable<T>> GetAllIQueryableAsync();
    }
}
