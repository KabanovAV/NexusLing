namespace NexusLing.Application.DTOs
{
    public record UpdateUserDTO(Guid Id, string FirstName, string LastName, string Login, string Password);
}
