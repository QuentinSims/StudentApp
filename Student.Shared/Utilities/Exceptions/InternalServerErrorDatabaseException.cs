using System.Runtime.Serialization;

namespace Student.Shared.Utilities.Exceptions
{
    //
    // Summary:
    //     The exception that is thrown a request to the database failed. Represents a 500
    //     error
    [Serializable]
    public class InternalServerErrorDatabaseException : Exception
    {
        public string Reason { get; private set; } = string.Empty;


        public InternalServerErrorDatabaseException()
        {
        }

        public InternalServerErrorDatabaseException(string reason)
        : base("Internal Error Database request failed.")
        {
            Reason = reason;
        }

        //
        // Summary:
        //     Add a inner exception to the new InternalServerErrorDatabaseException
        public InternalServerErrorDatabaseException(string reason, Exception inner)
            : base("Internal Error Database request failed.", inner)
        {
            Reason = reason;
        }

        protected InternalServerErrorDatabaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
