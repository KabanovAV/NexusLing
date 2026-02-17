namespace NexusLing.Domain.Exceptions
{
    public class NotFoundException : DomainException
    {
        public NotFoundException(string entityName, object id)
            : base($"Сущность '{entityName}' с Id '{id}' не найдена") { }
    }
}
