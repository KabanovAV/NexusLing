using NexusLing.Domain.Common;

namespace NexusLing.Domain.ValueObjects
{
    public sealed record Login
    {
        public const int MaxLength = 64;

        public string Value { get; }

        private Login() { }

        private Login(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Логин не может быть пустым.");
            var normalized = value.Trim().ToLowerInvariant();
            if (normalized.Length > MaxLength)
                throw new DomainException($"Логин не может быть длиннее {MaxLength} символов.");
            if (normalized.Any(char.IsWhiteSpace))
                throw new DomainException("Логин не может содержать пробелы.");
            Value = value;
        }

        public static Login Create(string value)
            => new(value);

        public override string ToString() => Value;
    }
}