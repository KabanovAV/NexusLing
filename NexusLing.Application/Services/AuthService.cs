using NexusLing.Application.Common.Interfaces;
using NexusLing.Application.DTOs;
using NexusLing.Application.Interfaces;

namespace NexusLing.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _service;
        private readonly IValidationService _validationService;

        public AuthService(IUserService service, IValidationService validationService)
        {
            _service = service;
            _validationService = validationService;
        }

        public Task<string> LoginAsync(LoginDTO login)
        {
            throw new NotImplementedException();
        }
    }
}
