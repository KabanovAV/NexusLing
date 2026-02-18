using NexusLing.Application.Bases;

namespace NexusLing.Application.DTOs
{
    public record UserDTO(Guid Id, string FirstName, string LastName, string Login)
        : UserBaseDTO(FirstName, LastName, Login);
}
