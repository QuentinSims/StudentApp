using System.Runtime.Serialization;

namespace Student.Shared.Utilities.Exceptions
{
    //
    // Summary:
    //     The exception that is thrown when the code has a logic error. Represents a 500
    //     error
    [Serializable]
    public class InternalServerErrorProcessException : Exception
    {
        public string Reason { get; private set; } = string.Empty;


        public InternalServerErrorProcessException()
        {
        }

        public InternalServerErrorProcessException(string reason)
        : base("Internal error while processing data.")
        {
            Reason = reason;
        }

        //
        // Summary:
        //     Add a inner exception to the new InternalServerErrorProcessException
        public InternalServerErrorProcessException(string reason, Exception inner)
            : base("Internal error while processing data.", inner)
        {
            Reason = reason;
        }

        protected InternalServerErrorProcessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
