namespace NexusLing.Domain
{
    public class BaseAuditableEntity : BaseEntity
    {
        public DateTime DateCreated { get; set; }
        public DateTime DetaUpdated { get; set; }
    }
}
