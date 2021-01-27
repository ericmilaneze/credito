using System.Net;
using Credito.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Credito.WebApi.ModelProviders
{
    public class ProducesResponseTypeDefaultErrorsModelProvider : IApplicationModelProvider
    {
        public int Order => 1;

        public void OnProvidersExecuted(ApplicationModelProviderContext context)
        { }

        public void OnProvidersExecuting(ApplicationModelProviderContext context)
        {
            foreach (var controller in context.Result.Controllers)
            {
                foreach (var action in controller.Actions)
                {
                    action.Filters.Add(new ProducesResponseTypeAttribute(typeof(ValidationErrorModel), (int)HttpStatusCode.BadRequest));
                    action.Filters.Add(new ProducesResponseTypeAttribute(typeof(DefaultErrorModel), (int)HttpStatusCode.InternalServerError));
                }
            }
        }
    }
}