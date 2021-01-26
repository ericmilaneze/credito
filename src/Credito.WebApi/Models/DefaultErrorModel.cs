namespace Credito.WebApi.Models
{
    public record DefaultErrorModel
    {
        public string Message { get; }
        public string TraceIdentifier { get; }

        public DefaultErrorModel(string message, string traceIdentifier)
        {
            Message = message;
            TraceIdentifier = traceIdentifier;
        }
    }
}