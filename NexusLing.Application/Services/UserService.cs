using NexusLing.Application.Interfaces;
using NexusLing.Domain.Entities;
using NexusLing.Domain.Interfaces;

namespace NexusLing.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;

        public UserService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
            => await _repository.UserRepository.GetAllUserAsync();

        public async Task<User> GetUserAsync(Guid id)
            => await _repository.UserRepository.GetUserAsync(id);

        public async Task<User> AddUserAsync(User user)
            => await _repository.UserRepository.AddUserAsync(user);

        public async Task UpdateUserAsync(User user)
            => await _repository.UserRepository.UpdateUserAsync(user);

        public async Task DeleteUserAsync(User user)
            => await _repository.UserRepository.DeleteUserAsync(user);
    }
}
