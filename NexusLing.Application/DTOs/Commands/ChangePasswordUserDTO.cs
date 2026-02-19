namespace NexusLing.Application.DTOs
{
    public record ChangePasswordUserDTO(Guid Id, string Password, string ConfirmPassword);
}
