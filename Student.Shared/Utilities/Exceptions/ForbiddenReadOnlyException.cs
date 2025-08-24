using System.Runtime.Serialization;

namespace Student.Shared.Utilities.Exceptions
{
    //
    // Summary:
    //     The exception that is thrown when a an edit is attempted on a read only record.
    //     Represents a 403 Forbidden entry
    [Serializable]
    public class ForbiddenReadOnlyException : Exception
    {
        public string Reason { get; private set; } = string.Empty;


        public ForbiddenReadOnlyException()
        {
        }

        public ForbiddenReadOnlyException(string reason)
        : base("The store attempted to modify a read-only record.")
        {
            Reason = reason;
        }

        //
        // Summary:
        //     Add a inner exception to the new BadRequestModelException
        public ForbiddenReadOnlyException(string reason, Exception inner)
            : base("The store attempted to modify a read-only record.", inner)
        {
            Reason = reason;
        }

        protected ForbiddenReadOnlyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
