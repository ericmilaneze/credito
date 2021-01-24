using System;

namespace Credito.Application.Common.Exceptions
{
    public class ResourceException : Exception
    {
        public object Request { get; }

        public ResourceException(object request, string message) : base(message) =>
            Request = request;
    }
}