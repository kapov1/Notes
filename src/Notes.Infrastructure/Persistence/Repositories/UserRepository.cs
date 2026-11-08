namespace Notes.Infrastructure.Persistence.Repositories;

public class UserRepository(
    AppDbContext dbContext) : IUserRepository
{
    private IQueryable<User> ApplyFilters(
        IQueryable<User> users,
        UserQueryParameters queryParameters)
    {
        if (queryParameters.Name is not null)
        {
            users = users
                .Where(u => u.Name.Contains(queryParameters.Name));
        }
    
        return users;
    }

    private IQueryable<User> ApplySorting(
        IQueryable<User> users,
        UserQueryParameters queryParameters)
    {
        if (queryParameters.SortBy.Count == 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        var sortDirection = queryParameters.SortDirection;
        var isAscending = sortDirection == ListSortDirection.Ascending;

        var firstSortExpression = 
            GetSortExpression(queryParameters.SortBy[0]);

        var sortedUsers = isAscending
            ? users.OrderBy(firstSortExpression)
            : users.OrderByDescending(firstSortExpression);

        for (int i = 1; i < queryParameters.SortBy.Count; i++)
        {
            var sortExpression = 
                GetSortExpression(queryParameters.SortBy[i]);

            sortedUsers  = isAscending
                ? sortedUsers.ThenBy(sortExpression)
                : sortedUsers.ThenByDescending(sortExpression);
        }

        return sortedUsers;
    }

    private Expression<Func<User, object>> GetSortExpression(
        UserSortBy sortBy)
    {
        return sortBy switch
        {
            UserSortBy.Name => u => u.Name,
            _ => u => u.Name
        };       
    }

    public async Task<Result<User>> CreateAsync(
        User user, 
        CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.Users.AddAsync(
                user, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result<User>.Success(user);
        }
        catch(DbUpdateException)
        {
            return Result<User>.Failure("Failed to create user.");
        }
    }

    public async Task<Result> DeleteAsync(
        int id, 
        CancellationToken cancellationToken)
    {
        var getUserResult = 
            await GetByIdAsync(id, cancellationToken);

        if (getUserResult.IsFailure)
        {
            return getUserResult;
        }

        dbContext.Users.Remove(getUserResult.Value); 
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<User>> GetByIdAsync(
        int id, 
        CancellationToken cancellationToken)
    {
        var user = 
            await dbContext.Users
                .FirstOrDefaultAsync(
                    n => n.Id == id, cancellationToken);

        return user is null
            ? Result<User>.Failure(
                "The user with this ID was not found.")
            : Result<User>.Success(user);
    }

    public async Task<PagedResult<User>> GetPagedAsync(
        UserQueryParameters queryParameters, 
        CancellationToken cancellationToken)
    {
        IQueryable<User> users = 
            dbContext.Users.AsNoTracking();

        users = ApplyFilters(users, queryParameters);

        int totalCount = 
            await users.CountAsync(cancellationToken);

        users = ApplySorting(users, queryParameters);

        var pagedUsers = await users  
            .Skip((queryParameters.Page - 1) * queryParameters.PageSize)
            .Take(queryParameters.PageSize)
            .ToListAsync(cancellationToken);

        return new(
            pagedUsers,
            totalCount,
            queryParameters.Page,
            queryParameters.PageSize);
    }

    public async Task<Result<User>> UpdateDetailsAsync(
        int id, Action<User> updateAct, 
        CancellationToken cancellationToken)
    {
        var getUserResult = 
            await GetByIdAsync(id, cancellationToken);

        if (getUserResult.IsFailure)
        {
            return getUserResult;
        }

        var user = getUserResult.Value;
        updateAct(user);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<User>.Success(user);
    }
}
