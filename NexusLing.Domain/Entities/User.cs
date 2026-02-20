using NexusLing.Domain.Common;
using NexusLing.Domain.Exceptions;
using NexusLing.Domain.ValueObjects;

namespace NexusLing.Domain.Entities
{
    public class User : AuditableEntityBase
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Login Login { get; private set; }
        public PasswordHash PasswordHash { get; private set; }

        public User() { }

        private User(string firstName, string lastName, Login login, PasswordHash passwordHash)
        {
            SetFullName(firstName, lastName);
            Login = login ?? throw new DomainException("Логин не может быть пустым.");
            PasswordHash = passwordHash ?? throw new DomainException("Пароль не может быть пустым."); ;
        }

        public void SetFullName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("Имя не может быть пустым.");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("Фамилия не может быть пустым.");
            FirstName = firstName;
            LastName = lastName;
        }

        public static User Create(string firstName, string lastName, string login, PasswordHash passwordHash)
            => new(firstName, lastName, Login.Create(login), passwordHash);


        public bool ApplyUpdate(string? firstName, string? lastName, string? login)
        {
            var hasChanges = false;
            if (firstName != null && firstName != FirstName)
            {
                FirstName = firstName;
                hasChanges = true;
            }
            if (lastName != null && lastName != LastName)
            {
                LastName = lastName;
                hasChanges = true;
            }
            if (login != null && login != Login.Value)
            {
                Login = Login.Create(login);
                hasChanges = true;
            }
            return hasChanges;
        }

        public void ChangePassword(PasswordHash newPassword)
            => PasswordHash = newPassword;
    }
}
