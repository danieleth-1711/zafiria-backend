namespace Zafiria.Application.DTOs;

public class ApiResponse<T>
{
    public string Status { get; set; } = string.Empty;
    public int Code { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Timestamp { get; set; } = DateTime.UtcNow.ToString("o");
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = new();

    public static ApiResponse<T> Success(T data, string message = "Operación exitosa", int code = 200)
    {
        return new ApiResponse<T>
        {
            Status = "success",
            Code = code,
            Message = message,
            Data = data,
            Errors = new List<string>()
        };
    }

    public static ApiResponse<T> Error(string message, int code = 400, List<string>? errors = null)
    {
        return new ApiResponse<T>
        {
            Status = "error",
            Code = code,
            Message = message,
            Data = default,
            Errors = errors ?? new List<string>()
        };
    }
}