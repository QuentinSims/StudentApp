using System.Runtime.Serialization;

namespace Student.Shared.Utilities.Exceptions
{
    //
    // Summary:
    //     The exception that is thrown when a entity is not found.
    [Serializable]
    public class NotFoundException : Exception
    {
        public long Id { get; private set; }

        public string Entity { get; private set; } = string.Empty;


        public NotFoundException()
        {
        }

        //
        // Summary:
        //     The message that describes the exception
        public NotFoundException(string message)
            : base(message)
        {
        }

        //
        // Summary:
        //     The message that describes the exception Add a inner exception to the new BadRequestModelException
        public NotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        //
        // Summary:
        //     Serialization Info Streaming Context
        public NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public NotFoundException(string entity, long id)
        : base($"Unable to retrieve record {id} from {entity}.")
        {
            Id = id;
            Entity = entity;
        }
    }
}
