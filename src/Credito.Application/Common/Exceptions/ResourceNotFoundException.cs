namespace Credito.Application.Common.Exceptions
{
    public class ResourceNotFoundException : ResourceException
    {
        public ResourceNotFoundException(object request)
            : base(request, "The resource was not found.")
        { }
    }
}