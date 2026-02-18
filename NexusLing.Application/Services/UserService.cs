using NexusLing.Application.Common.Exceptions;
using NexusLing.Application.Common.Mappings;
using NexusLing.Application.DTOs;
using NexusLing.Application.Interfaces;
using NexusLing.Application.Validators;
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

        public UserService(IRepository repository, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
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
            if (user == null)
                throw new NotFoundException("User", id);
            return user.ToDto();
        }

        /// <summary>
        /// Добавить одиного пользователя в набор данных
        /// </summary>
        /// <param name="rUser">Добавляемый пользователь</param>
        /// <returns>Объект после добавления в БД</returns>
        public async Task<UserDTO> AddUserAsync(RegisterUserDTO rUser)
        {
            var validator = new RegisterUserValidator();
            var validatorResult = await validator.ValidateAsync(rUser);

            if (!validatorResult.IsValid)
            {
                var errorResponse = validatorResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
                throw new ValidatorException(errorResponse);
            }                

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
            var user = await _repository.UserRepository.GetUserAsync(id);
            user.ApplyUpdate(uUser.FirstName, uUser.LastName, uUser.Login);
            if (!_passwordHasher.Verify(uUser.Password, user.PasswordHash.Value))
                user.ChangePassword(uUser.Password);
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
