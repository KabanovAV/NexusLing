using FluentValidation;
using NexusLing.Application.DTOs;
using NexusLing.Domain.Interfaces;

namespace NexusLing.Application.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserValidator(IRepository repository)
        {
            Include(new UserBaseValidator<UpdateUserDTO>(repository));
        }
    }
}
