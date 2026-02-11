using NexusLing.Domain.Entities;

namespace NexusLing.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс с операциями для репозитория пользователь
    /// </summary>
    public interface IUserRepository : IRepositoryOperations<User, Guid>
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
    }
}
