using Microsoft.EntityFrameworkCore;
using NexusLing.Application.Common.Interfaces.Repositories;
using NexusLing.Infrastructure.Database;

namespace NexusLing.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext Context { get; set; }
        private DbSet<T> DbSet => Context.Set<T>();

        public Repository(ApplicationDbContext context)
        {
            Context = context;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}
