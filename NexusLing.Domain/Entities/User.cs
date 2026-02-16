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

        private
    }
}
