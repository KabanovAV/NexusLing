using FluentValidation;
using NexusLing.Application.Bases;
using NexusLing.Application.Common;

namespace NexusLing.Application.Validators
{
    public class UserBaseValidator<T> : AbstractValidator<T> where T : UserBaseDTO
    {
        public UserBaseValidator()
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
        }
    }
}
