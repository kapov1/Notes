namespace Notes.Shared.Results;

public partial class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string ErrorMessage { get; } = string.Empty;

    protected Result() => IsSuccess = true;

    protected Result(string errorMessage)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(errorMessage);

        IsSuccess = false;
        ErrorMessage = errorMessage;
    }

    public static Result Success() => new();
    public static Result Failure(string errorMessage) => new(errorMessage);
}