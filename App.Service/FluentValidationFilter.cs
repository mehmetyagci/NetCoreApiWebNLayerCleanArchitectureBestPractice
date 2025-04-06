using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace App.Service;

public class FluentValidationFilter : IAsyncActionFilter
{
    private readonly IServiceProvider _serviceProvider;
    
    public FluentValidationFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var argument in context.ActionArguments)
        {
            var argumentType = argument.Value?.GetType();
            if (argumentType == null) continue;

            var validatorType = typeof(IValidator<>).MakeGenericType(argumentType);
            var validator = _serviceProvider.GetService(validatorType);
            if (validator == null) continue;

            var validationContext = new ValidationContext<object>(argument.Value);

            // Sadece işaretli action'larda id'yi ekle
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var hasUseIdAttr = actionDescriptor?.MethodInfo
                .GetCustomAttributes(typeof(UseIdInValidationAttribute), false)
                .Any() ?? false;
            
            if (hasUseIdAttr &&
                context.RouteData.Values.TryGetValue("id", out var idValue) &&
                idValue is string idStr &&
                int.TryParse(idStr, out var id))
            {
                validationContext.RootContextData["Id"] = id;
            }

            var validationResult = await ((IValidator)validator).ValidateAsync(validationContext);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                var resultModel = ServiceResult.Fail(errors);
                context.Result = new BadRequestObjectResult(resultModel);
                return;
            }
        }

        await next();
    }
}