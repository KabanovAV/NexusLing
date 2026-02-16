using NexusLing.Domain.Common;
using NexusLing.Domain.ValueObjects;

namespace NexusLing.Domain.Entities
{
    public class User : AuditableEntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Login Login { get; set; }
        public string Password { get; set; }
    }
}
