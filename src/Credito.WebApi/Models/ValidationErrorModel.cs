using System.Collections.Generic;

namespace Credito.WebApi.Models
{
    public record ValidationErrorModel
    {
        public string Message { get; init; }
        public string TraceIdentifier { get; init; }
        public IEnumerable<ValidationErrorFieldModel> Fields { get; init; }
    }
}