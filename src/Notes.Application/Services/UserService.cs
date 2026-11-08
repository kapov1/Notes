namespace Notes.Application.Services;

public class UserService(
    IUserRepository userRepository) : IUserService
{
    public async Task<UserResponse> CreateAsync(
        CreateUserRequest createRequest, 
        CancellationToken cancellationToken)
    {
        var createdUser = await userRepository.CreateAsync(
            new(createRequest.Name),
            cancellationToken);

        UserResponse response = new(createdUser);

        return response;
    }

    public async Task<Result> DeleteAsync(
        int id, 
        CancellationToken cancellationToken)
    {
        return await userRepository.DeleteAsync(
            id, 
            cancellationToken);
    }

    public async Task<Result<UserResponse>> GetByIdAsync(
        int id, 
        CancellationToken cancellationToken)
    {
        var getUserResult = await userRepository.GetByIdAsync(
            id, 
            cancellationToken);

        if (getUserResult.IsFailure)
        {
            return Result<UserResponse>.Failure(
                getUserResult.ErrorMessage);
        }

        UserResponse response = new(getUserResult.Value);

        return Result<UserResponse>.Success(response);
    }

    public async Task<PagedResult<UserResponse>> GetPagedAsync(
        UserQueryParameters queryParameters, 
        CancellationToken cancellationToken)
    {
        var pagedUsers = await userRepository.GetPagedAsync(
            queryParameters,
            cancellationToken);

        var usersResponses = pagedUsers.Items.Select(
            user => new UserResponse(user));

        return new(
            usersResponses,
            pagedUsers.TotalCount,
            pagedUsers.Page,
            pagedUsers.PageSize);
    }

    public async Task<Result<UserResponse>> UpdateAsync(
        int id, 
        UpdateUserRequest updateRequest,
        CancellationToken cancellationToken)
    {
        var updateUserResult = 
            await userRepository.UpdateDetailsAsync(
                id,
                user =>
                {
                    user.UpdateName(updateRequest.Name);
                },
                cancellationToken);

        if (updateUserResult.IsFailure)
        {
            return Result<UserResponse>.Failure(
                updateUserResult.ErrorMessage);
        }

        var updatedUser = updateUserResult.Value;
        UserResponse response = new(updatedUser);

        return Result<UserResponse>.Success(response);
    }
}
