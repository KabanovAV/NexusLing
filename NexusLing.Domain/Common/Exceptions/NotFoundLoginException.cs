namespace NexusLing.Domain.Exceptions
{
    public class NotFoundLoginException : DomainException
    {
        public NotFoundLoginException(string entityName, string login)
            : base($"Сущность '{entityName}' с логином '{login}' не найдена") { }
    }
}
