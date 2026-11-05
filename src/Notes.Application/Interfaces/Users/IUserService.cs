namespace Notes.Application.Interfaces.Users;

public interface IUserService
{
    Task<PagedResult<UserResponse>> GetPagedAsync(
        UserQueryParameters queryParameters,
        CancellationToken cancellationToken);

    Task<Result<UserResponse>> GetByIdAsync(
        int id, 
        CancellationToken cancellationToken);

    Task<Result<UserResponse>> CreateAsync(
        CreateUserRequest createRequest,
        CancellationToken cancellationToken);

    Task<Result<UserResponse>> UpdateAsync(
        int id,
        UpdateUserRequest updateRequest,
        CancellationToken cancellationToken);

    Task<Result> DeleteAsync(
        int id,
        CancellationToken cancellationToken);
}
