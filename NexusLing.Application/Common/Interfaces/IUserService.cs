using NexusLing.Application.DTOs;

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
        Task<IEnumerable<UserDTO>> GetAllUserAsync();

        /// <summary>
        /// Получение одного пользователя из набора данных
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns>Возвращает одного пользователя из набора данных</returns>
        Task<UserDTO> GetUserAsync(Guid id);

        /// <summary>
        /// Добавить одиного пользователя в набор данных
        /// </summary>
        /// <param name="rUser">Добавляемый пользователь</param>
        /// <returns>Объект после добавления в БД</returns>
        Task<UserDTO> AddUserAsync(RegisterUserDTO rUser);

        /// <summary>
        /// Изменить одиного пользователя в наборе данных
        /// </summary>
        /// <param name="uUser">Изменяемый пользователь</param>
        Task UpdateUserAsync(Guid id, UpdateUserDTO uUser);

        /// <summary>
        /// Изменить пароль пользователя
        /// </summary>
        /// <param name="uPassword">Новый пароль пользователя</param>
        Task UpdatePasswordAsync(Guid id, ChangePasswordDTO uPassword);

        /// <summary>
        /// Удалить одоного пользователя из набора данных
        /// </summary>
        /// <param name="id">Id пользователя</param>
        Task DeleteUserAsync(Guid id);
    }
}
