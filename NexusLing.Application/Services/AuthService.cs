using NexusLing.Application.Common.Interfaces;
using NexusLing.Application.DTOs;
using NexusLing.Application.Interfaces;
using NexusLing.Domain.Exceptions;
using NexusLing.Domain.ValueObjects;

namespace NexusLing.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _service;
        private readonly IJwtProvider _jwtProvider;
        private readonly IValidationService _validationService;

        public AuthService(IUserService service, IJwtProvider jwtProvider, IValidationService validationService)
        {
            _service = service;
            _jwtProvider = jwtProvider;
            _validationService = validationService;
        }

        public async Task<string> LoginAsync(LoginDTO request)
        {
            await _validationService.ValidateAndThrowAsync(request);
            var login = Login.Create(request.Login);
            var user = await _service.GetUserByLoginAsync(login.Value) ?? throw new NotFoundException("User", "логин", login.Value);
            string token = _jwtProvider.Generate(user);
            return token;
        }
    }
}
