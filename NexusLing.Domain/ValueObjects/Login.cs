using NexusLing.Domain.Common;
using NexusLing.Domain.Exceptions;

namespace NexusLing.Domain.ValueObjects
{
    public sealed class Login : ValueObject
    {
        public const int MaxLength = 64;

        public string Value { get; }

        private Login(string value)
        {
            Value = value;
        }

        public static Login Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Логин не может быть пустым.");
            var normalized = value.Trim().ToLowerInvariant();
            if (normalized.Length > MaxLength)
                throw new DomainException($"Логин не может быть длиннее {MaxLength} символов.");
            if (normalized.Any(char.IsWhiteSpace))
                throw new DomainException("Логин не может содержать пробелы.");
            return new(normalized);
        }

        public override string ToString() => Value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}