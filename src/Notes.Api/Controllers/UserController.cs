namespace Notes.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(
    IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<Ok<PagedResult<UserResponse>>> GetPagedAsync(
        [FromQuery] UserQueryParameters queryParameters,
        CancellationToken cancellationToken)
    {
        var pagedUsers = await userService.GetPagedAsync(
            queryParameters, 
            cancellationToken);

        return TypedResults.Ok(pagedUsers);
    }

    [HttpGet("{id:int}", Name = "GetUserById")]
    public async Task<Results<Ok<UserResponse>, NotFound<string>>> GetByIdAsync(
        int id, 
        CancellationToken cancellationToken)
    {
        var getUserResult = await userService.GetByIdAsync(
            id, 
            cancellationToken);

        return getUserResult.IsSuccess
            ? TypedResults.Ok(getUserResult.Value)
            : TypedResults.NotFound(getUserResult.ErrorMessage);
    }

    [HttpPost]
    public async Task<CreatedAtRoute<UserResponse>> CreateAsync(
        [FromBody] CreateUserRequest createRequest,
        CancellationToken cancellationToken)
    {
        var createdUser = await userService.CreateAsync(
            createRequest, 
            cancellationToken);

        return TypedResults.CreatedAtRoute(
            createdUser, 
            "GetUserById", 
            new { id = createdUser.Id });
    }

    [HttpPut("{id:int}")]
    public async Task<Results<Ok<UserResponse>, NotFound<string>>> UpdateAsync(
        int id,
        [FromBody] UpdateUserRequest updateRequest,
        CancellationToken cancellationToken)
    {
        var updateUserResult = await userService.UpdateAsync(
            id, 
            updateRequest, 
            cancellationToken);

        return updateUserResult.IsSuccess
            ? TypedResults.Ok(updateUserResult.Value)
            : TypedResults.NotFound(updateUserResult.ErrorMessage);
    }

    [HttpDelete("{id:int}")]
    public async Task<Results<NoContent, NotFound<string>>> DeleteAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var deleteResult = await userService.DeleteAsync(
            id, 
            cancellationToken);

        return deleteResult.IsSuccess
            ? TypedResults.NoContent()
            : TypedResults.NotFound(deleteResult.ErrorMessage);
    }
}
