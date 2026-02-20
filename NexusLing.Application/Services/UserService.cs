using NexusLing.Application.Common.Interfaces;
using NexusLing.Application.Common.Mappings;
using NexusLing.Application.DTOs;
using NexusLing.Application.Interfaces;
using NexusLing.Domain.Common.Exceptions;
using NexusLing.Domain.Entities;
using NexusLing.Domain.Exceptions;
using NexusLing.Domain.Interfaces;
using NexusLing.Domain.ValueObjects;

namespace NexusLing.Application.Services
{
    /// <summary>
    /// Сервис с операциями для обьекта пользователь
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IRepository _repository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IValidationService _validationService;

        public UserService(IRepository repository, IPasswordHasher passwordHasher, IValidationService validationService)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _validationService = validationService;
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
            return user == null ? throw new NotFoundException("User", id) : user.ToDto();
        }

        /// <summary>
        /// Добавить одиного пользователя в набор данных
        /// </summary>
        /// <param name="rUser">Добавляемый пользователь</param>
        /// <returns>Объект после добавления в БД</returns>
        public async Task<UserDTO> AddUserAsync(RegisterUserDTO rUser)
        {
            await _validationService.ValidateAndThrowAsync(rUser);
            var user = User.Create(rUser.FirstName, rUser.LastName, Login.Create(rUser.Login), PasswordHash.Create(_passwordHasher.Hash(rUser.Password)));
            await _repository.UserRepository.AddAsync(user);
            return user.ToDto();
        }

        /// <summary>
        /// Изменить одиного пользователя в наборе данных
        /// </summary>
        /// <param name="uUser">Изменяемый пользователь</param>
        public async Task UpdateUserAsync(Guid id, UpdateUserDTO uUser)
        {
            await _validationService.ValidateAndThrowAsync(uUser);
            if (id != uUser.Id)
                throw new IdNotEqualException(id, uUser.Id);
            var user = await _repository.UserRepository.GetUserAsync(id) ?? throw new NotFoundException("User", id);
            user.ApplyUpdate(uUser.FirstName, uUser.LastName, uUser.Login);
            await _repository.UserRepository.Update(user);
        }

        /// <summary>
        /// Изменить пароль пользователя
        /// </summary>
        /// <param name="uPassword">Новый пароль пользователя</param>
        public async Task UpdatePasswordAsync(Guid id, ChangePasswordDTO uPassword)
        {
            await _validationService.ValidateAndThrowAsync(uPassword);
            if (id != uPassword.Id)
                throw new IdNotEqualException(id, uPassword.Id);
            var user = await _repository.UserRepository.GetUserAsync(id) ?? throw new NotFoundException("User", id);
            user.ChangePassword(PasswordHash.Create(_passwordHasher.Hash(uPassword.Password)));
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
