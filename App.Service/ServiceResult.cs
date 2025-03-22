using System.Net;

namespace App.Service;

public class ServiceResult<T>
{
    /// <summary>
    /// Başarılı olduğunda Data dolucak
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Başarısız olduğunda hata mesajı dönecek
    /// </summary>
    public List<string>? ErrorMessage { get; set; }

    public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
    //public bool IsSuccess
    //{
    //    get
    //    {
    //        return ErrorMessage == null || ErrorMessage.Count == 0;
    //    }
    //}

    public bool IsFail => !IsSuccess;

    public HttpStatusCode Status { get; set; }

    /// static factory methods 
    /// new lemeyi kontrol altına almış olduk
    public static ServiceResult<T> Success(T data, HttpStatusCode status = HttpStatusCode.OK)
    {
        return new ServiceResult<T>()
        {
            Data = data,
            Status = status,
        };
    }

    public static ServiceResult<T> Fail(List<string> errorMessages, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>()
        {
            ErrorMessage = errorMessages,
            Status = status
        };
    }

    public static ServiceResult<T> Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>()
        {
            // ErrorMessage = new List<string> { errorMessage }
            ErrorMessage = [errorMessage],
            Status = status
        };
    }
}

public class ServiceResult
{
    /// <summary>
    /// Başarısız olduğunda hata mesajı dönecek
    /// </summary>
    public List<string>? ErrorMessage { get; set; }

    public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;

    public bool IsFail => !IsSuccess;

    public HttpStatusCode Status { get; set; }

    /// static factory methods 
    /// new lemeyi kontrol altına almış olduk
    public static ServiceResult Success(HttpStatusCode status = HttpStatusCode.OK)
    {
        return new ServiceResult()
        {
            Status = status,
        };
    }

    public static ServiceResult Fail(List<string> errorMessages, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult()
        {
            ErrorMessage = errorMessages,
            Status = status
        };
    }

    public static ServiceResult Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult()
        {
            ErrorMessage = [errorMessage],
            Status = status
        };
    }
}