namespace Notes.Shared.Results;

public class Result<T> : Result where T : notnull 
{
    private readonly T? _value;

    public T Value => IsSuccess 
        ? _value! 
        : throw new InvalidOperationException(
            "Cannot access Value of a failed result.");

    private Result(T value) : base()
    {
        ArgumentNullException.ThrowIfNull(value);
        _value = value;
    }

    private Result(string errorMessage) 
        : base(errorMessage)
    {       
    }

    public new static Result<T> Failure(string errorMessage) => new(errorMessage);
    public static Result<T> Success(T value) => new(value);
}