using System.Collections.Generic;

namespace Credito.WebApi.Models
{
    public record ValidationErrorFieldModel
    {
        public string Name { get; }
        public IEnumerable<string> ErrorMessage { get; }

        public ValidationErrorFieldModel(string name, IEnumerable<string> errorMessage)
        {
            Name = name;
            ErrorMessage = errorMessage;
        }
    }
}