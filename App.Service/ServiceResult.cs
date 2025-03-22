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
}
