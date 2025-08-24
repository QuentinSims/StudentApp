using System.Runtime.Serialization;

namespace Student.Shared.Utilities.Exceptions
{
    //
    // Summary:
    //     The exception that is thrown when a model member is invalid. Represents a 400
    //     error
    [Serializable]
    public class BadRequestModelException : Exception
    {
        private string _key = string.Empty;

        public string Key
        {
            get
            {
                return _key ?? string.Empty;
            }
            set
            {
                _key = value;
            }
        }

        public BadRequestModelException()
        {
        }

        //
        // Summary:
        //     The message that describes the exception
        public BadRequestModelException(string message)
            : base(message)
        {
        }

        //
        // Summary:
        //     The message that describes the exception Add a inner exception to the new BadRequestModelException
        public BadRequestModelException(string message, Exception inner)
            : base(message, inner)
        {
        }

        //
        // Summary:
        //     Serialization Info Streaming Context
        public BadRequestModelException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        //
        // Summary:
        //     The message that describes the exception
        public BadRequestModelException(string key, string message)
            : base(message)
        {
            _key = key;
        }
    }
}
