namespace NexusLing.Domain.Common
{
    public class AuditableEntityBase : EntityBase
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
