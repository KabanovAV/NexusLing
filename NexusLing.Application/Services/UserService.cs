using NexusLing.Application.Common.Mappings;
using NexusLing.Application.DTOs;
using NexusLing.Application.Interfaces;
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
        public async Task<IEnumerable<UserDTO>> GetAllUserAsync()
        {
            var users = await _repository.UserRepository.GetAllUserAsync();
            return users.ToDto();
        }

        /// <summary>
        /// Получение одного пользователя из набора данных
        /// </summary>
        /// <returns>Возвращает одного пользователя из набора данных</returns>
        public async Task<UserDTO> GetUserAsync(Guid id)
        {
            var user = await _repository.UserRepository.GetUserAsync(id);
            return user.ToDto();
        }

        /// <summary>
        /// Добавить одиного пользователя в набор данных
        /// </summary>
        /// <param name="rUser">Добавляемый пользователь</param>
        /// <returns>Объект после добавления в БД</returns>
        public async Task<UserDTO> AddUserAsync(RegisterUserDTO rUser)
        {
            var user = rUser.ToEntity();
            await _repository.UserRepository.AddAsync(user);
            return user.ToDto();
        }

        /// <summary>
        /// Изменить одиного пользователя в наборе данных
        /// </summary>
        /// <param name="uUser">Изменяемый пользователь</param>
        public async Task UpdateUserAsync(Guid id, UpdateUserDTO uUser)
        {
            var user = await _repository.UserRepository.GetUserAsync(id);
            user.UpdateDto(uUser);
            await _repository.UserRepository.Update(user);
        }

        /// <summary>
        /// Удалить одоного пользователя из набора данных
        /// </summary>
        /// <param name="id">Id пользователя</param>
        public async Task DeleteUserAsync(Guid id)
            => await _repository.UserRepository.Delete(id);
    }
}
