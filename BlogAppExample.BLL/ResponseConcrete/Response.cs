namespace BlogAppExample.BLL.ResponseConcrete;


public class Response
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }

    public static Response Success(string message = "")
    {
        return new Response { IsSuccess = true, Message = message };
    }

    public static Response Failure(string message)
    {
        return new Response { IsSuccess = false, Message = message };
    }
}

public class Response<T> : Response
where T : class
{
    public T Data { get; set; }

    public static new Response<T> Success(T data, string message = "")
    {
        return new Response<T> { IsSuccess = true, Message = message, Data = data };
    }

    public static new Response<T> Failure(string message)
    {
        return new Response<T> { IsSuccess = false, Message = message };
    }
}