using NexusLing.Application.DTOs;
using NexusLing.Application.Common;

namespace NexusLing.Application.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса операциями для обьекта пользователь
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Получение всех пользователей из набора данных
        /// </summary>
        /// <returns>Возвращает список всех пользователей из набора данных</returns>
        Task<Result<IEnumerable<UserDTO>>> GetAllUserAsync();

        /// <summary>
        /// Получение одного пользователя из набора данных по Id
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns>Возвращает одного пользователя из набора данных</returns>
        Task<Result<UserDTO>> GetUserByIdAsync(Guid id);

        /// <summary>
        /// Получение одного пользователя из набора данных по логину
        /// </summary>
        /// <param name="loginUser">Логин пользователя</param>
        /// <returns>Возвращает одного пользователя из набора данных</returns>
        Task<Result<UserDTO>> GetUserByLoginAsync(string loginUser);

        /// <summary>
        /// Добавить одиного пользователя в набор данных
        /// </summary>
        /// <param name="rUser">Добавляемый пользователь</param>
        /// <returns>Объект после добавления в БД</returns>
        Task<Result<UserDTO>> AddUserAsync(RegisterUserDTO rUser);

        /// <summary>
        /// Изменить одиного пользователя в наборе данных
        /// </summary>
        /// <param name="uUser">Изменяемый пользователь</param>
        Task<Result> UpdateUserAsync(Guid id, UpdateUserDTO uUser);

        /// <summary>
        /// Изменить пароль пользователя
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <param name="uPassword">Новый пароль пользователя</param>
        Task<Result> UpdatePasswordAsync(Guid id, ChangePasswordDTO uPassword);

        /// <summary>
        /// Удалить одоного пользователя из набора данных
        /// </summary>
        /// <param name="id">Id пользователя</param>
        Task DeleteUserAsync(Guid id);
    }
}
