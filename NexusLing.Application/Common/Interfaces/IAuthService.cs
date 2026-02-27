using NexusLing.Application.Common;
using NexusLing.Application.DTOs;

namespace NexusLing.Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result<string>> LoginAsync(LoginDTO request);
    }
}
