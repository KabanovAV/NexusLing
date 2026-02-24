using FluentValidation;
using NexusLing.Application.Common;
using NexusLing.Application.DTOs;

namespace NexusLing.Application.Validators
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(u => u.Login)
                    .NotEmpty().WithMessage(x => string.Format(ValidationMessages.Required, "Логин"))
                    .Length(3, 64).WithMessage(x => string.Format(ValidationMessages.LengthFromTo, "Логин", 3, 64));

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage(x => string.Format(ValidationMessages.Required, "Пароль"))
                .Length(8, 64).WithMessage(x => string.Format(ValidationMessages.Length, "Пароль", 8));
        }
    }
}
