namespace NexusLing.Domain.Interfaces
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
