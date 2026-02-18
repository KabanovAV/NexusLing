using NexusLing.Domain.Exceptions;

namespace NexusLing.Domain.Common.Exceptions
{
    public class IdNotEqualException : DomainException
    {
        public IdNotEqualException(object expectedId, object actualId)
            : base($"Несовпадение идентификаторов: ожидался '{expectedId}', получен '{actualId}'") { }
    }
}
