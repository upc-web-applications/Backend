namespace RiskGuard.Platform.Shared.Application.Model;

public class Result<T>
{
    protected Result(bool isSuccess, T? value, string message, string? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Message = message;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public string Message { get; }
    public string? Error { get; }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, string.Empty, null);
    }

    public static Result<T> Failure(string error, string message)
    {
        return new Result<T>(false, default, message, error);
    }
}

public class Result : Result<object>
{
    private Result(bool isSuccess, string message, string? error) : base(isSuccess, null, message, error)
    {
    }

    public static Result Success()
    {
        return new Result(true, string.Empty, null);
    }

    public static Result Failure(string error, string message)
    {
        return new Result(false, message, error);
    }
}
