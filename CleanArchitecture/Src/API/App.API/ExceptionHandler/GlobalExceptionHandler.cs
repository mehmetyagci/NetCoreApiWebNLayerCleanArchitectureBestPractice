using System.Net;
using App.Service;
using Microsoft.AspNetCore.Diagnostics;

namespace App.API.ExceptionHandler;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine("GlobalExceptionHandler -> ");
        
        var errorAsDto = ServiceResult.Fail(exception.Message, HttpStatusCode.InternalServerError);

        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(errorAsDto, cancellationToken);

        return true; // Middleware 'i sonlandırıyoruz. Bir sonraki Middleware 'e geçmeyecek. 
    }
}