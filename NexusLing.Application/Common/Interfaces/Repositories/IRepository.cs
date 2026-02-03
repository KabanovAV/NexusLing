namespace NexusLing.Application.Common.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Delete(Guid id);
        void DeleteRange(IEnumerable<Guid> ids);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
    }
}
