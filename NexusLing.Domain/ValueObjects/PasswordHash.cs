using NexusLing.Domain.Common;
using NexusLing.Domain.Exceptions;

namespace NexusLing.Domain.ValueObjects
{
    public sealed class PasswordHash : ValueObject
    {
        public string Value { get; }

        private PasswordHash(string value)
        {
            Value = value;
        }

        public static PasswordHash Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Хэш пароля не может быть пустым.");
            if (!value.StartsWith("$PBKDF2$"))
                throw new DomainException("Некорректный формат хэша.");
            if (value.Length > 256)
                throw new DomainException("Хэш пароля превышает допустимую длину.");
            return new(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
