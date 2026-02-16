using NexusLing.Domain.Common;
using NexusLing.Domain.ValueObjects;

namespace NexusLing.Domain.Entities
{
    public class User : AuditableEntityBase
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Login Login { get; private set; }
        public PasswordHash PasswordHash { get; private set; }

        private User() { }

        private User(string firstName, string lastName, Login login, PasswordHash passwordHash)
        {
            SetFullName(firstName, lastName);
            Login = login ?? throw new DomainException("Логин не может быть пустым.");
            PasswordHash = passwordHash ?? throw new DomainException("Пароль не может быть пустым."); ;
        }

        public static User Create(string firstName, string lastName, Login login, PasswordHash passwordHash)
            => new(firstName, lastName, login, passwordHash);

        public void SetFullName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("Имя не может быть пустым.");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("Фамилия не может быть пустым.");
            FirstName = firstName;
            LastName = lastName;
        }

        public void ChangeFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("Имя не может быть пустым.");
            FirstName = firstName;
        }

        public void ChangeLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("Фамилия не может быть пустым.");
            LastName = lastName;
        }

        public void ChangeLogin(Login login)
            => Login = login ?? throw new DomainException("Логин не может быть пустым.");

        public void ChangePassword(PasswordHash passwordHash)
            => PasswordHash = passwordHash ?? throw new DomainException("Пароль не может быть пустым.");
    }
}
