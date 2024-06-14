namespace InternSystem.Application.Common.Persistences.IRepositories.IBaseRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(object id);
        Task<T> GetTruongHocsByTenAsync(object name);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> GetAllASync();

        void Remove(T entity);
        void Update(T entity);
    }
}
