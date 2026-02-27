using NexusLing.Application.Common;
using NexusLing.Application.Common.Interfaces;
using NexusLing.Application.Common.Mappings;
using NexusLing.Application.DTOs;
using NexusLing.Application.Interfaces;
using NexusLing.Domain.Entities;
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
        public async Task<Result<IEnumerable<UserDTO>>> GetAllUserAsync()
        {
            var users = await _repository.UserRepository.GetAllUserAsync();
            return users.ToDto();
        }

        /// <summary>
        /// Получение одного пользователя из набора данных по Id
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns>Возвращает одного пользователя из набора данных</returns>
        public async Task<Result<UserDTO>> GetUserByIdAsync(Guid id)
        {
            var user = await _repository.UserRepository.GetUserByIdAsync(id);
            if (user == null)
                return Result.Failure<UserDTO>(Error.NotFound("User.NotFound", $"Пользователь с идентификатором '{id}' не найден"));
            return user.ToDto();
        }

        /// <summary>
        /// Получение одного пользователя из набора данных по логину
        /// </summary>
        /// <param name="loginUser">Логин пользователя</param>
        /// <returns>Возвращает одного пользователя из набора данных</returns>
        public async Task<Result<UserDTO>> GetUserByLoginAsync(string loginUser)
        {
            var login = Login.Create(loginUser);
            var user = await _repository.UserRepository.GetUserByLoginAsync(login.Value);
            if (user == null)
                return Result.Failure<UserDTO>(Error.NotFound("User.NotFoundByLogin", $"Пользователь с логином '{login.Value}' не найден"));
            return user.ToDto();
        }

        /// <summary>
        /// Добавить одиного пользователя в набор данных
        /// </summary>
        /// <param name="rUser">Добавляемый пользователь</param>
        /// <returns>Объект после добавления в БД</returns>
        public async Task<Result<UserDTO>> AddUserAsync(RegisterUserDTO rUser)
        {
            var result = await _validationService.ValidateAsync(rUser);
            if (result.IsSuccess)
            {
                var user = User.Create(rUser.FirstName, rUser.LastName, rUser.Login, _passwordHasher.Hash(rUser.Password));
                await _repository.UserRepository.AddAsync(user);
                return user.ToDto();
            }
            return Result.Failure<UserDTO>(result.Error);
        }

        /// <summary>
        /// Изменить одиного пользователя в наборе данных
        /// </summary>
        /// <param name="uUser">Изменяемый пользователь</param>
        public async Task<Result> UpdateUserAsync(Guid id, UpdateUserDTO uUser)
        {
            if (id != uUser.Id)
                return Result.Failure(Error.Conflict("User.NotEqualId", $"Несовпадение идентификаторов: ожидался '{id}', получен '{uUser.Id}'"));

            var result = await _validationService.ValidateAsync(uUser);
            if (result.IsSuccess)
            {
                var user = await _repository.UserRepository.GetUserByIdAsync(id);
                if (user == null)
                    return Result.Failure(Error.NotFound("User.NotFound", $"Пользователь с идентификатором '{id}' не найден"));
                if (user.ApplyUpdate(uUser.FirstName, uUser.LastName, uUser.Login))
                    await _repository.UserRepository.Update(user);
                return Result.Success();
            }
            return Result.Failure(result.Error);
        }

        /// <summary>
        /// Изменить пароль пользователя
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <param name="uPassword">Новый пароль пользователя</param>
        public async Task<Result> UpdatePasswordAsync(Guid id, ChangePasswordDTO uPassword)
        {
            if (id != uPassword.Id)
                return Result.Failure(Error.Conflict("Password.NotEqualId", $"Несовпадение идентификаторов: ожидался '{id}', получен '{uPassword.Id}'"));

            var result = await _validationService.ValidateAsync(uPassword);
            if (result.IsSuccess)
            {
                var user = await _repository.UserRepository.GetUserByIdAsync(id);
                if (user == null)
                    return Result.Failure(Error.NotFound("User.NotFound", $"Пользователь с идентификатором '{id}' не найден"));
                user.ChangePassword(_passwordHasher.Hash(uPassword.Password));
                await _repository.UserRepository.Update(user);
            }
            return Result.Failure(result.Error);
        }

        /// <summary>
        /// Удалить одоного пользователя из набора данных
        /// </summary>
        /// <param name="id">Id пользователя</param>
        public async Task DeleteUserAsync(Guid id)
            => await _repository.UserRepository.Delete(id);
    }
}
