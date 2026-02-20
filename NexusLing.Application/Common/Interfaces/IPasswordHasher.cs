using NexusLing.Domain.ValueObjects;

namespace NexusLing.Application.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса хэширования пароля
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Хэширование пароля
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <returns>Возвращает хэшированный пароль</returns>
        PasswordHash Hash(string password);

        /// <summary>
        /// Верификаци пароля
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <param name="passwordHash">Хэшированный пароль</param>
        /// <returns>Возвращает true если пароль прошел верификацию, false не прошел</returns>
        bool Verify(string password, PasswordHash passwordHash);
    }
}
