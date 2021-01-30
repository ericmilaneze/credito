using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading;
using Credito.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using Serilog;

namespace Credito.WebApi.ModelProviders
{
    public class ProducesResponseTypeDefaultErrorsModelProvider : IApplicationModelProvider
    {
        private static readonly ILogger _logger = Log.ForContext(typeof(ProducesResponseTypeDefaultErrorsModelProvider));

        public int Order => 1;

        public void OnProvidersExecuted(ApplicationModelProviderContext context)
        { }

        public void OnProvidersExecuting(ApplicationModelProviderContext context)
        {
            foreach (var controller in context.Result.Controllers)
            {
                foreach (var action in controller.Actions)
                {
                    TryAddDefaultErrorsFilters(action);
                    TryAddProduces201Filter(action);
                    TryAddProducesFilter(action);
                    TryAddConsumesFilter(action);
                }
            }
        }

        private void TryAddDefaultErrorsFilters(ActionModel action)
        {
            if (HasAnyProducesReponseWithStatusCode(action, HttpStatusCode.BadRequest))
            {
                LogProducesResponseTypeFilterAlreadyExists(action, "BadRequest");
            }
            else
            {
                LogAddingProducesResponseTypeFilter(action, "BadRequest");
                action.Filters.Add(new ProducesResponseTypeAttribute(typeof(ValidationErrorModel),
                                                                    (int)HttpStatusCode.BadRequest));
            }

            if (HasAnyProducesReponseWithStatusCode(action, HttpStatusCode.InternalServerError))
            {
                LogProducesResponseTypeFilterAlreadyExists(action, "InternalServerError");
            }
            else
            {
                LogAddingProducesResponseTypeFilter(action, "InternalServerError");
                action.Filters.Add(new ProducesResponseTypeAttribute(typeof(DefaultErrorModel),
                                                                    (int)HttpStatusCode.InternalServerError));
            }
        }

        private void TryAddProduces201Filter(ActionModel action)
        {
            if (HasAnyProducesReponseWithStatusCode(action, HttpStatusCode.Created))
            {
                LogProducesResponseTypeFilterAlreadyExists(action, "Created");
                return;
            }

            var returnType = GetReturnType(action) ?? GetResultReturnType(action);

            if (returnType is null)
                return;

            if (returnType.IsAssignableFrom(typeof(CreatedAtRouteResult)) || returnType.IsSubclassOf(typeof(CreatedAtRouteResult)))
            {
                LogAddingProducesResponseTypeFilter(action, "Created");
                action.Filters.Add(new ProducesResponseTypeAttribute((int)HttpStatusCode.Created));
            }
        }

        private void TryAddProducesFilter(ActionModel action)
        {
            if (HasAnyProduces(action))
            {
                LogFilterAlreadyExists(action, "ProducesAttribute");
                return;
            }

            var returnType = GetReturnType(action) ?? GetResultReturnType(action);

            if (returnType is null)
                return;

            if (!returnType.IsAssignableFrom(typeof(FileResult)) && !returnType.IsSubclassOf(typeof(FileResult)))
            {
                LogAddingFilter(action, "ProducesAttribute", "application/json");
                action.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
            }
        }

        private void TryAddConsumesFilter(ActionModel action)
        {
            if (HasAnyConsumes(action))
            {
                LogFilterAlreadyExists(action, "ConsumesAttribute");
                return;
            }

            var allowedMethodVerbs = new string[] { "POST", "UPDATE" };
            if (allowedMethodVerbs.Any(x => GetMethodVerbs(action).Contains(x)))
            {
                if (AnyActionParametersExist(action))
                {
                    LogAddingFilter(action, "ConsumesAttribute", "application/json");
                    action.Filters.Add(new ConsumesAttribute(MediaTypeNames.Application.Json));
                }
            }
        }

        private Type GetReturnType(ActionModel action)
        {
            var resultReturnType = GetResultReturnType(action);

            if (resultReturnType == default)
                return default;

            var genericArguments = resultReturnType.GetGenericArguments();
            
            if (!genericArguments.Any())
                return default;

            return genericArguments[0];
        }

        private Type GetResultReturnType(ActionModel action)
        {
            if (!action.ActionMethod.ReturnType.GenericTypeArguments.Any())
                return default;

            var genericTypeArguments = action.ActionMethod.ReturnType.GenericTypeArguments;

            if (!genericTypeArguments.Any())
                return default;
            
            return genericTypeArguments[0];
        }

        private bool AnyActionParametersExist(ActionModel action) =>
            action.Parameters.Any(x => !x.ParameterType.IsAssignableFrom(typeof(CancellationToken)));

        private IEnumerable<string> GetMethodVerbs(ActionModel action) =>
            action.Attributes.OfType<HttpMethodAttribute>()
                             .SelectMany(x => x.HttpMethods)
                             .Distinct();

        private bool HasAnyProducesReponseWithStatusCode(ActionModel action, HttpStatusCode statusCode) =>
            action.Filters.Any(x => (x as ProducesResponseTypeAttribute)?.StatusCode == (int)statusCode);

        private bool HasAnyProduces(ActionModel action) =>
            action.Filters.Any(x => (x as ProducesAttribute) != null);

        private bool HasAnyConsumes(ActionModel action) =>
            action.Filters.Any(x => (x as ConsumesAttribute) != null);

        private void LogAddingProducesResponseTypeFilter(ActionModel action, string statusCodeName) =>
            _logger.Verbose($"Adding \"ProducesResponseTypeAttribute\" for \"{statusCodeName}\" on action {{ControllerName}}/{{ActionName}}.",
                            action.Controller.ControllerName,
                            action.ActionName);

        private void LogProducesResponseTypeFilterAlreadyExists(ActionModel action, string statusCodeName) =>
            _logger.Verbose($"There's already a \"ProducesResponseTypeAttribute\" for \"{statusCodeName}\" on action {{ControllerName}}/{{ActionName}}. Filter for \"{statusCodeName}\" not added.",
                            action.Controller.ControllerName,
                            action.ActionName);

        private void LogAddingFilter(ActionModel action, string attributeName, string details) =>
            _logger.Verbose($"Adding \"{attributeName}\" filter on {{ControllerName}}/{{ActionName}}. Details: {details}.",
                            action.Controller.ControllerName,
                            action.ActionName);

        private void LogFilterAlreadyExists(ActionModel action, string attributeName) =>
            _logger.Verbose($"There's already a \"{attributeName}\" filter on action {{ControllerName}}/{{ActionName}}. Filter not added.",
                            action.Controller.ControllerName,
                            action.ActionName);
    }
}