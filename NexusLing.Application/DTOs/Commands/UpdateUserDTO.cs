using NexusLing.Application.Bases;

namespace NexusLing.Application.DTOs
{
    public record UpdateUserDTO(Guid Id, string FirstName, string LastName, string Login, string Password)
        : UserBaseDTO(FirstName, LastName, Login);
}
