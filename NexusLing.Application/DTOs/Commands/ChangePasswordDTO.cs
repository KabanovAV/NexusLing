namespace NexusLing.Application.DTOs
{
    public record ChangePasswordDTO(Guid Id, string Password, string ConfirmPassword);
}
