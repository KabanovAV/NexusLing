using System.ComponentModel.DataAnnotations;

namespace NexusLing.Application.DTOs
{
    public record RegisterUserDTO()
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Login { get; init; }
        public string Password { get; init; }
        public string ConfirmPassword { get; init; }
    };
}
