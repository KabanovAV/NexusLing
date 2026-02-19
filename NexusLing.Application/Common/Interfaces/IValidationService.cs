using FluentValidation.Results;

namespace NexusLing.Application.Common.Interfaces
{
    internal interface IValidationService
    {
        /// <summary>
        /// Валидирует объект и возвращает результат
        /// </summary>
        Task<ValidationResult> ValidateAsync<T>(T instance);

        /// <summary>
        /// Валидирует объект и выбрасывает исключение при ошибках
        /// </summary>
        Task ValidateAndThrowAsync<T>(T instance);
    }
}
