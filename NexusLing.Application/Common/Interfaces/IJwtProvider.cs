using NexusLing.Application.DTOs;

namespace NexusLing.Application.Interfaces
{
    /// <summary>
    /// Интерфейс Jwt провайдера
    /// </summary>
    public interface IJwtProvider
    {
        /// <summary>
        /// Генерация токена
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Возвращает токен</returns>
        string Generate(UserDTO user);
    }
}
