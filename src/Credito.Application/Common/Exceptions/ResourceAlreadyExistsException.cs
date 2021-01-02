using System;

namespace Credito.Application.Common.Exceptions
{
    public class ResourceAlreadyExistsException : Exception
    {
        public object Request { get; }

        public ResourceAlreadyExistsException(object request)
            : base("The resource already exists.")
        {
            Request = request;
        }
    }
}