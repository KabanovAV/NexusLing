using FluentValidation;
using NexusLing.Application.Bases;
using NexusLing.Application.Common;
using NexusLing.Domain.Interfaces;

namespace NexusLing.Application.Validators
{
    public class UserBaseValidator<T> : AbstractValidator<T> where T : UserBaseDTO
    {
        private readonly IRepository _repository;

        public UserBaseValidator(IRepository repository)
        {
            _repository = repository;

            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage(x => string.Format(ValidationMessages.Required, "Имя"))
                .Matches(@"^[a-zA-Zа-яА-Я\s\-]+$").WithMessage("Имя может содержать только буквы, пробелы и дефис");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage(x => string.Format(ValidationMessages.Required, "Фамилия"))
                .Matches(@"^[a-zA-Zа-яА-Я\s\-]+$").WithMessage("Фамилия может содержать только буквы, пробелы и дефис");

            RuleFor(u => u.Login)
                .NotEmpty().WithMessage(x => string.Format(ValidationMessages.Required, "Логин"))
                .Length(3, 64).WithMessage(x => string.Format(ValidationMessages.LengthFromTo, "Логин", 3, 64))
                .MustAsync(BeExistLogin).WithMessage("Пользователь с таким логином уже существует");
        }

        private async Task<bool> BeExistLogin(string login, CancellationToken cancellationToken)
        {
            var user = await _repository.UserRepository.GetByLoginAsync(login);
            return user == null;
        }
    }
}
