using System.ComponentModel.DataAnnotations;

namespace NexusLing.Application.DTOs
{
    public record UpdateUserDTO()
    {
        public Guid Id { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Login { get; init; }
        public string? Password { get; init; }
    };
}
