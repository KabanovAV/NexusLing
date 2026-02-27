using NexusLing.Application.Common;
using NexusLing.Application.DTOs;

namespace NexusLing.Application.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса авторизации и идентификации пользователя
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="request">Параметры логирования</param>
        /// <returns>Возвращает результат авторизации, при успешной авторизации возвращает токен</returns>
        Task<Result<string>> LoginAsync(LoginDTO request);
    }
}
