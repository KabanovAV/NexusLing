namespace NexusLing.Domain.Exceptions
{
    public class NotFoundIdException : DomainException
    {
        public NotFoundIdException(string entityName, object id)
            : base($"Сущность '{entityName}' с Id '{id}' не найдена") { }
    }
}
