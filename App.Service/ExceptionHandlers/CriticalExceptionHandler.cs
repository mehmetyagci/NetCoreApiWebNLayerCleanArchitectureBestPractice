using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace App.Service.ExceptionHandlers;

/// <summary>
/// email ve sms bildirimi yapmak istiyorum.
/// sonrasıda global exception handler e geçecek süreç
/// </summary>
public class CriticalExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        Console.WriteLine("CriticalExceptionHandler -> ");
        // business logic
        if (exception is CriticalException)
        {
            Console.WriteLine("hata ile ilgil sms gönderildi.");
        }

        // return true; // Hatayı ben ele aldım, hatayı bir sonraki middleware 'e aktarma
        return new ValueTask<bool>(false); // Hatayı bir sonraki middleware 'e aktar. GlobalExceptionHandler 'e
    }
}