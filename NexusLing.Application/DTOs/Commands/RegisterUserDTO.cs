using System.ComponentModel.DataAnnotations;

namespace NexusLing.Application.DTOs
{
    public record RegisterUserDTO()
    {
        [Required(ErrorMessage = "Обязательно для заполнения")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Поле содержит только символы")]
        public string FirstName { get; init; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Поле содержит только символы")]
        public string LastName { get; init; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [StringLength(64, ErrorMessage = "Максимальная длина символов 64")]
        public string Login { get; init; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [StringLength(64, MinimumLength = 8, ErrorMessage = "Пароль должен быть от 8 до 64 символов")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&]).{8,}$",
            ErrorMessage = "Пароль должен состоять как минимум из 8 символов и включать заглавные и строчные буквы, цифру и специальный символ")]
        public string Password { get; init; }

        [Required(ErrorMessage = "Подтверждение пароля обязательно")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; init; }
    };
}
