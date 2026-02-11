namespace NexusLing.Application.DTOs.Commands
{
    public record UpdateUserDTO(Guid Id, string FirstName, string LastName, string Login, string Password);
}
