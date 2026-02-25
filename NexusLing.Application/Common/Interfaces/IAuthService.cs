using NexusLing.Application.DTOs;

namespace NexusLing.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDTO login);
    }
}
