using NexusLing.Domain.Interfaces;

namespace NexusLing.Domain.Common.Interfaces.Repositories
{
    /// <summary>
    /// Интерфейс к репозиториям данных
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Пользователи
        /// </summary>
        IUserRepository UserRepository { get; }
    }
}
