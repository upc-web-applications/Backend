namespace Acme.Center.Platform.Shared.Application.Model;

public class Result<T>
{
    protected Result(bool isSuccess, T? value, string message, object? error)
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
    public object? Error { get; }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, string.Empty, null);
    }

    public static Result<T> Failure(object error, string message)
    {
        return new Result<T>(false, default, message, error);
    }
}

public class Result : Result<object>
{
    private Result(bool isSuccess, string message, object? error) : base(isSuccess, null, message, error)
    {
    }

    public static Result Success()
    {
        return new Result(true, string.Empty, null);
    }

    public new static Result Failure(object error, string message)
    {
        return new Result(false, message, error);
    }
}
