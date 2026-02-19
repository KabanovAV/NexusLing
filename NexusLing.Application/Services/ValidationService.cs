using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using NexusLing.Application.Common.Exceptions;
using NexusLing.Application.Common.Interfaces;

namespace NexusLing.Application.Services
{
    /// <summary>
    /// Сервис с операциями для валидации объектов
    /// </summary>
    public class ValidationService : IValidationService
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Валидирует объект и выбрасывает исключение при ошибках
        /// </summary>  
        /// <typeparam name="T">Тип обьекта</typeparam>
        /// <param name="instance">Обьект валидации</param>
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

        /// <summary>
        /// Валидирует объект и возвращает результат
        /// </summary>
        /// <typeparam name="T">Тип обьекта</typeparam>
        /// <param name="instance">Обьект валидации</param>
        public async Task<ValidationResult> ValidateAsync<T>(T instance)
        {
            var validator = _serviceProvider.GetService<IValidator<T>>();
            if (validator == null)
                return new ValidationResult();
            return await validator.ValidateAsync(instance);
        }
    }
}
