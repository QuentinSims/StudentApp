using System.Text.Json;

namespace Student.Shared.Utilities.Exceptions
{

    public class ErrorResponseDetails
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
