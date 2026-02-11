using NexusLing.Application.Interfaces;
using NexusLing.Domain.Entities;
using NexusLing.Domain.Interfaces;

namespace NexusLing.Application.Services
{
    /// <summary>
    /// Сервис с операциями для обьекта пользователь
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IRepository _repository;

        public UserService(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получение всех пользователей из набора данных
        /// </summary>
        /// <returns>Возвращает список всех пользователей из набора данных</returns>
        public async Task<IEnumerable<User>> GetAllUserAsync()
            => await _repository.UserRepository.GetAllUserAsync();

        /// <summary>
        /// Получение одного пользователя из набора данных
        /// </summary>
        /// <returns>Возвращает одного пользователя из набора данных</returns>
        public async Task<User> GetUserAsync(Guid id)
            => await _repository.UserRepository.GetUserAsync(id);

        /// <summary>
        /// Добавить одиного пользователя в набор данных
        /// </summary>
        /// <param name="user">Добавляемый пользователь</param>
        /// <returns>Объект после добавления в БД</returns>
        public async Task<User> AddUserAsync(User user)
            => await _repository.UserRepository.AddUserAsync(user);

        /// <summary>
        /// Изменить одиного пользователя в наборе данных
        /// </summary>
        /// <param name="user">Изменяемый пользователь</param>
        public async Task UpdateUserAsync(User user)
            => await _repository.UserRepository.UpdateUserAsync(user);

        /// <summary>
        /// Удалить одиного пользователя из набора данных
        /// </summary>
        /// <param name="user">Удаляемый пользователь</param>
        public async Task DeleteUserAsync(User user)
            => await _repository.UserRepository.DeleteUserAsync(user);
    }
}
