using App.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.Service.Filters;

public class NotFoundFilter<T, TId>(IGenericRepository<T, TId> genericRepository) : Attribute, IAsyncActionFilter 
    where T : class
    where TId : struct
{

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var idValue = context.ActionArguments.Values.FirstOrDefault();
        if (idValue is null) // ilk parametre boş ise ActionFilter çalışmaya devam etsin.  
        {
            await next();
            return;
        }
        
        var idKey = context.ActionArguments.Keys.FirstOrDefault(); // "id" key değeri
        if (idKey != "id")   
        {
            await next();
            return;
        }

        if ((idValue is not TId id))
        {
            await next();
            return;
        }
        
        var anyEntity = await genericRepository.AnyAsync(id);
        if (!anyEntity)
        {
            var entityName = typeof(T).Name;
            var actionName = context.ActionDescriptor.RouteValues["action"];

            var result = ServiceResult.Fail($"data bulunamamıştır.({entityName}, {actionName}), {id.ToString()}");
            context.Result = new NotFoundObjectResult(result);
            return;
        }
        
        
        // Action metod çalışmadan önce
        await next();
        // Action metod çalıştıkran sonra

    }
}