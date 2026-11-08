namespace Notes.Application.Interfaces.Services;

public interface IUserService
{
    Task<PagedResult<UserResponse>> GetPagedAsync(
        UserQueryParameters queryParameters,
        CancellationToken cancellationToken);

    Task<Result<UserResponse>> GetByIdAsync(
        int id, 
        CancellationToken cancellationToken);

    Task<UserResponse> CreateAsync(
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
