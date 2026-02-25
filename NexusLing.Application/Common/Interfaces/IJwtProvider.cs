using NexusLing.Application.DTOs;

namespace NexusLing.Application.Interfaces
{
    public interface IJwtProvider
    {
        string Generate(UserDTO user);
    }
}
