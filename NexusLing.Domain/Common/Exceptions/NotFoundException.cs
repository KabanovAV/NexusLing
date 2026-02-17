namespace NexusLing.Domain.Exceptions
{
    internal class NotFoundException : DomainException
    {
        public NotFoundException(string message)
            : base(message) { }
    }
}
