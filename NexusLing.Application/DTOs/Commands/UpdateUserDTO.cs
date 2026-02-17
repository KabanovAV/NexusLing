using System.ComponentModel.DataAnnotations;

namespace NexusLing.Application.DTOs
{
    public record UpdateUserDTO()
    {
        public Guid Id { get; init; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Поле содержит только символы")]
        public string? FirstName { get; init; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Поле содержит только символы")]
        public string? LastName { get; init; }

        [StringLength(64, ErrorMessage = "Максимальная длина символов 64")]
        public string? Login { get; init; }

        [StringLength(64, MinimumLength = 8, ErrorMessage = "Пароль должен быть от 8 до 64 символов")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&]).{8,}$",
            ErrorMessage = "Пароль должен состоять как минимум из 8 символов и включать заглавные и строчные буквы, цифру и специальный символ")]
        public string? Password { get; init; }
    };
}
