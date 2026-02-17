namespace NexusLing.Domain.Exceptions
{
    internal class NotFoundException : DomainException
    {
        public NotFoundException(string entityName, object id)
            : base($"Сущность '{entityName}' с Id '{id}' не найдена") { }
    }
}
