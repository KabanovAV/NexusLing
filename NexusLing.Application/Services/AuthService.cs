using NexusLing.Application.Common;
using NexusLing.Application.Common.Interfaces;
using NexusLing.Application.DTOs;
using NexusLing.Application.Interfaces;
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

        public async Task<Result<string>> LoginAsync(LoginDTO request)
        {
            var result = await _validationService.ValidateAsync(request);
            if (result.IsSuccess)
            {
                var login = Login.Create(request.Login);
                var userResult = await _service.GetUserByLoginAsync(login.Value);
                if (userResult.IsFailure)
                    Result.Failure<string>(userResult.Error);
                string token = _jwtProvider.Generate(userResult.Value);
                return token;
            }
            return Result.Failure<string>(result.Error);
        }
    }
}
