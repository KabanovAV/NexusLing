using FluentValidation.Results;

namespace NexusLing.Application.Common.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса операциями для валидации объектов
    /// </summary>
    public interface IValidationService
    {
        /// <summary>
        /// Валидирует объект и возвращает результат
        /// </summary>
        /// <typeparam name="T">Тип обьекта</typeparam>
        /// <param name="instance">Обьект валидации</param>
        Task<ValidationResult> ValidateAsync<T>(T instance);

        /// <summary>
        /// Валидирует объект и выбрасывает исключение при ошибках
        /// </summary>  
        /// <typeparam name="T">Тип обьекта</typeparam>
        /// <param name="instance">Обьект валидации</param>
        Task ValidateAndThrowAsync<T>(T instance);
    }
}
