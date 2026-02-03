namespace NexusLing.Domain.Common
{
    public class BaseAuditableEntity : BaseEntity
    {
        public DateTime DateCreated { get; set; }
        public DateTime DetaUpdated { get; set; }
    }
}
