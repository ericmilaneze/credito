using System;

namespace Credito.Application.Common.Exceptions
{
    public class ResourceAlreadyExistsException : ResourceException
    {
        public ResourceAlreadyExistsException(object request)
            : base(request, "The resource already exists.")
        { }
    }
}