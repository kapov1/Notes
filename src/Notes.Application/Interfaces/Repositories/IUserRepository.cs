namespace Notes.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<Result<User>> CreateAsync(
        User user,
        CancellationToken cancellationToken);

    Task<PagedResult<User>> GetPagedAsync(
        UserQueryParameters queryParameters,
        CancellationToken cancellationToken);
    
    Task<Result<User>> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<Result<User>> UpdateDetailsAsync(
        int id,
        Action<User> updateAct,
        CancellationToken cancellationToken);

    Task<Result> DeleteAsync(
        int id, 
        CancellationToken cancellationToken);
}
