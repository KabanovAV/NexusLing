using NexusLing.Application.Bases;

namespace NexusLing.Application.DTOs
{
    public record RegisterUserDTO(string FirstName, string LastName, string Login, string Password, string ConfirmPassword)
        : UserBaseDTO(FirstName, LastName, Login);
}
