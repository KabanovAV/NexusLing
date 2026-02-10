using Microsoft.EntityFrameworkCore;
using NexusLing.Domain.Entities;
using NexusLing.Domain.Interfaces;
using NexusLing.Infrastructure.Database;

namespace NexusLing.Infrastructure.Repositories
{
    /// <summary>
    /// Базовые операциии доступные для репозитория пользователь
    /// </summary>
    public class UserRepository : Repository<User, Guid>, IUserRepository
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

        /// <summary>
        /// Добавить одиного пользователя в набор данных
        /// </summary>
        /// <param name="user">Добавляемый пользователь</param>
        /// <returns>Объект после добавления в БД</returns>
        public async Task<User> AddUserAsync(User user)
            => await AddAsync(user);

        /// <summary>
        /// Изменить одиного пользователя в наборе данных
        /// </summary>
        /// <param name="user">Изменяемый пользователь</param>
        public async Task UpdateUserAsync(User user)
            => await Update(user);

        /// <summary>
        /// Удалить одиного пользователя из набора данных
        /// </summary>
        /// <param name="user">Удаляемый пользователь</param>
        public async Task DeleteUserAsync(User user)
            => await Delete(user);
    }
}
