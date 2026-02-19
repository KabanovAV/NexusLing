namespace NexusLing.Application.DTOs
{
    public record PasswordChangeUserDTO(Guid Id, string Password, string ConfirmPassword);
}
