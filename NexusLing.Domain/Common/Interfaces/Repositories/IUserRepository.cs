using NexusLing.Domain.Entities;
using NexusLing.Domain.Interfaces;

namespace NexusLing.Domain.Common.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        /// <summary>
        /// Получение всех пользователей из набора данных
        /// </summary>
        /// <returns>Возвращает список всех пользователей из набора данных</returns>
        Task<IEnumerable<User>> GetAllUserAsync();
        /// <summary>
        /// Получение одного пользователя из набора данных
        /// </summary>
        /// <returns>Возвращает одного пользователя из набора данных</returns>
        Task<User> GetUserAsync(Guid id);
        /// <summary>
        /// Добавить одиного пользователя в набор данных
        /// </summary>
        /// <param name="user">Добавляемый пользователь</param>
        /// <returns>Объект после добавления в БД</returns>
        Task<User> AddUserAsync(User user);
        /// <summary>
        /// Изменить одиного пользователя в наборе данных
        /// </summary>
        /// <param name="user">Изменяемый пользователь</param>
        void UpdateUserAsync(User user);
        /// <summary>
        /// Удалить одиного пользователя из набора данных
        /// </summary>
        /// <param name="user">Удаляемый пользователь</param>
        void DeleteUserAsync(User user);
    }
}
