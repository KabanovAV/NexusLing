using NexusLing.Domain.Exceptions;

namespace NexusLing.Domain.Common.Exceptions
{
    public class NotEqualIdException : DomainException
    {
        public NotEqualIdException(object expectedId, object actualId)
            : base($"Несовпадение идентификаторов: ожидался '{expectedId}', получен '{actualId}'") { }
    }
}
