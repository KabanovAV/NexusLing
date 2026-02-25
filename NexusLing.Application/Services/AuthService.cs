using NexusLing.Application.Common.Interfaces;
using NexusLing.Application.DTOs;
using NexusLing.Domain.Interfaces;

namespace NexusLing.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository _repository;
        private readonly IValidationService _validationService;

        public AuthService(IRepository repository, IValidationService validationService)
        {
            _repository = repository;
            _validationService = validationService;
        }

        public Task<string> LoginAsync(LoginDTO login)
        {
            throw new NotImplementedException();
        }
    }
}
