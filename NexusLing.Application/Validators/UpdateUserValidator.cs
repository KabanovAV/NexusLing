using FluentValidation;
using NexusLing.Application.Common;
using NexusLing.Application.DTOs;

namespace NexusLing.Application.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage(x => string.Format(ValidationMessages.Required, "Имя"))
                .Matches(@"^[a-zA-Zа-яА-Я\s\-]+$").WithMessage("Имя может содержать только буквы, пробелы и дефис");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage(x => string.Format(ValidationMessages.Required, "Фамилия"))
                .Matches(@"^[a-zA-Zа-яА-Я\s\-]+$").WithMessage("Фамилия может содержать только буквы, пробелы и дефис");

            RuleFor(u => u.Login)
                .NotEmpty().WithMessage(x => string.Format(ValidationMessages.Required, "Логин"))
                .Length(3, 64).WithMessage(x => string.Format(ValidationMessages.Length, "Логин", 3, 64));

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage(x => string.Format(ValidationMessages.Required, "Пароль"))
                .Length(8, 64).WithMessage(x => string.Format(ValidationMessages.Length, "Пароль", 8, 64))
                .Must(u => u.Any(char.IsUpper)).WithMessage("Пароль должен содержать заглавную букву")
                .Must(u => u.Any(char.IsLower)).WithMessage("Пароль должен содержать строчную букву")
                .Must(u => u.Any(char.IsDigit)).WithMessage("Пароль должен содержать цифру")
                .Must(u => u.Any(ch => !char.IsLetterOrDigit(ch))).WithMessage("Пароль должен содержать специальный символ");
        }
    }
}
