using FluentValidation;
using NexusLing.Application.Common;
using NexusLing.Application.DTOs;
using NexusLing.Domain.Interfaces;

namespace NexusLing.Application.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserValidator(IRepository repository)
        {
            Include(new UserBaseValidator<UpdateUserDTO>(repository));

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
