using NexusLing.Domain.Common;

namespace NexusLing.Domain.ValueObjects
{
    public sealed record PasswordHash
    {
        public const int MinLength = 8;
        public const int MaxLength = 128;

        public string Value { get; }

        private PasswordHash() { }

        private PasswordHash(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Пароль не может быть пустым.");
            if (value.Length < MinLength)
                throw new DomainException($"Пароль должен содержать минимум {MinLength} символов.");
            if (value.Length > MaxLength)
                throw new DomainException($"Пароль не может быть длиннее {MaxLength} символов.");
            Value = value;
        }

        public static PasswordHash Create(string value)
            => new(value);
    }
}
