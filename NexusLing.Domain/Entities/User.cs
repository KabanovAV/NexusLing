using NexusLing.Domain.Common;

namespace NexusLing.Domain.Entities
{
    public class User : BaseAuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
