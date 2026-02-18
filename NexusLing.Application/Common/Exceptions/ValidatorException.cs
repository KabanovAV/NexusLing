namespace NexusLing.Application.Common.Exceptions
{
    public class ValidatorException : Exception
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; }

        public ValidatorException(IReadOnlyDictionary<string, string[]> errors)
            : base("Ошибка валидации входных данных")
        {
            Errors = new Dictionary<string, string[]>(errors);
        }
    }
}
