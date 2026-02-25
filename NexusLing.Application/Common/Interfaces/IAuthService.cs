using NexusLing.Application.DTOs;

namespace NexusLing.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDTO request);
    }
}
