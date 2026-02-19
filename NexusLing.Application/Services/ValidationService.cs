using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using NexusLing.Application.Common.Exceptions;
using NexusLing.Application.Common.Interfaces;

namespace NexusLing.Application.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ValidateAndThrowAsync<T>(T instance)
        {
            var validator = _serviceProvider.GetService<IValidator<T>>();
            if (validator == null)
                return;

            var validatorResult = await validator.ValidateAsync(instance);
            if (!validatorResult.IsValid)
            {
                var errorResponse = validatorResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
                throw new ValidatorException(errorResponse);
            }
        }

        public async Task<ValidationResult> ValidateAsync<T>(T instance)
        {
            var validator = _serviceProvider.GetService<IValidator<T>>();
            if (validator == null)
                return new ValidationResult();
            return await validator.ValidateAsync(instance);
        }
    }
}
