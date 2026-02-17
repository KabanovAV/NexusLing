using NexusLing.Domain.Common;
using NexusLing.Domain.Exceptions;

namespace NexusLing.Domain.ValueObjects
{
    public sealed class PasswordHash : ValueObject
    {
        public const int MinLength = 8;
        public const int MaxLength = 128;

        public string Value { get; }

        private PasswordHash(string value)
        {
            Value = value;
        }

        public static PasswordHash Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Пароль не может быть пустым.");
            if (value.Length < MinLength)
                throw new DomainException($"Пароль должен содержать минимум {MinLength} символов.");
            if (value.Length > MaxLength)
                throw new DomainException($"Пароль не может быть длиннее {MaxLength} символов.");
            return new(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
