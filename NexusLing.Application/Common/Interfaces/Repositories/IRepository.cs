namespace NexusLing.Application.Common.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Полные данные объекта, включая данные других объектов через внешние связи
        /// Может быть переопределён в конкретной реализации объекта данных
        /// По умолчанию соответствует значению PlainData
        /// </summary>
        IQueryable<T> Data { get; }
        /// <summary>
        /// Данные только одной таблицы.
        /// Объекты (поля) связей с другими таблицами не заполнены (null)
        /// </summary>
        IQueryable<T> PlainData { get; }
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
