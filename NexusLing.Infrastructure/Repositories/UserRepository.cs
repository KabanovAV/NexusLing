using Microsoft.EntityFrameworkCore;
using NexusLing.Domain.Entities;
using NexusLing.Domain.Interfaces;
using NexusLing.Infrastructure.Database;

namespace NexusLing.Infrastructure.Repositories
{
    /// <summary>
    /// Базовые операциии доступные для репозитория пользователь
    /// </summary>
    public class UserRepository : RepositoryOperations<User, Guid>, IUserRepository
    {
        public UserRepository(ApplicationDbContext db)
            : base(db) { }

        /// <summary>
        /// Получение всех пользователей из набора данных
        /// </summary>
        /// <returns>Возвращает список всех пользователей из набора данных</returns>
        public async Task<IEnumerable<User>> GetAllUserAsync()
            => await PlainData.ToListAsync();

        /// <summary>
        /// Получение одного пользователя из набора данных
        /// </summary>
        /// <returns>Возвращает одного пользователя из набора данных</returns>
        public async Task<User> GetUserAsync(Guid id)
            => await GetAsync(id);
    }
}
