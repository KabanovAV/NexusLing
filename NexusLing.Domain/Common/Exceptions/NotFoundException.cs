namespace NexusLing.Domain.Exceptions
{
    public class NotFoundException : DomainException
    {
        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string entityName, object id)
            : base($"Сущность '{entityName}' с идентификатором '{id}' не найдена") { }

        public NotFoundException(string entityName, string criteriaName, string criteriaValue)
            : base($"Сущность '{entityName}' с {criteriaName} '{criteriaValue}' не найдена") { }
    }
}
