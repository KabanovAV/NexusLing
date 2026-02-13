namespace NexusLing.Domain.Common
{
    public class EntityBase
    {
        public Guid Id { get; protected set; }

        public EntityBase()
        {
            Id = Guid.NewGuid();
        }
    }
}
