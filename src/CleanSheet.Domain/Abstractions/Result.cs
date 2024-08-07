namespace CleanSheet.Domain.Abstractions;
public record Result
{
    protected internal Result(bool isSuccess, Error? error = default)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public Error? Error { get; }

    public static Result Success() => new(true, null);
    public static Result Failure(Error error) => new(false, error);
    public static Result<TValue> Success<TValue>(TValue value) => new(true, value: value);
    public static Result<TValue> Failure<TValue>(Error error) => new(false, error);
}

public record Result<TValue> : Result
{
    protected internal Result(bool isSuccess, Error? error = default, TValue? value = default)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public TValue? Value { get; }
}
