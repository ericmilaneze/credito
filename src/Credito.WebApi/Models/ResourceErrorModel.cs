namespace Credito.WebApi.Models
{
    public record ResourceErrorModel
    {
        public string Message { get; }
        public string Request { get; }
        public string TraceIdentifier { get; }

        public ResourceErrorModel(string message, string request, string traceIdentifier)
        {
            Message = message;
            Request = request;
            TraceIdentifier = traceIdentifier;
        }
    }
}