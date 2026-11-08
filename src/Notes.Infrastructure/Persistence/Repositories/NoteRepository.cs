namespace Notes.Infrastructure.Persistence.Repositories;

public class NoteRepository(
    AppDbContext dbContext) : INoteRepository
{
    private IQueryable<Note> ApplyFilters(
        IQueryable<Note> query, 
        NoteQueryParameters queryParameters)
    {
        if (queryParameters.AuthorId is not null)
        {
            query = query.Where(
                n => n.AuthorId == queryParameters.AuthorId);
        }
        if (queryParameters.Title is not null)
        {
            query = query.Where(
                n => n.Title.ToLower().Contains(queryParameters.Title.ToLower()));
        }
        if (queryParameters.CreatedFrom is not null)
        {
            query = query.Where(
                n => n.CreatedAt >= queryParameters.CreatedFrom);
        }
        if (queryParameters.CreatedTo is not null)
        {
            query = query.Where(
                n => n.CreatedAt <= queryParameters.CreatedTo);
        }
        if (queryParameters.Tags is not null && 
            queryParameters.Tags.Any())
        {
            query = query.Where(
                n => n.Tags.All(
                    tag => queryParameters.Tags.Contains(tag)));
        }

        return query;
    }

    private Expression<Func<Note, object>> GetSortExpression(
        NoteSortBy sortBy)
    {
        return sortBy switch
        {
            NoteSortBy.Date => n => n.CreatedAt,
            NoteSortBy.Title => n => n.Title,
            NoteSortBy.TagsCount => n => n.Tags.Count,
            _ => n => n.CreatedAt
        };
    }

    private IQueryable<Note> ApplySorting(
        IQueryable<Note> query, 
        NoteQueryParameters queryParameters)
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
            ? query.OrderBy(firstSortExpression)
            : query.OrderByDescending(firstSortExpression);
            
        for (int i = 1; i < queryParameters.SortBy.Count; i++)
        {
            var sortExpression = 
                GetSortExpression(queryParameters.SortBy[i]);

            sortedUsers = isAscending
                ? sortedUsers.ThenBy(sortExpression)
                : sortedUsers.ThenByDescending(sortExpression);
        }

        return sortedUsers;
    }

    public async Task<Note> CreateAsync(
        Note note, 
        CancellationToken cancellationToken)
    {
        await dbContext.Notes.AddAsync(
            note, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return note;
    }

    public async Task<Result> DeleteAsync(
        int id, 
        CancellationToken cancellationToken)
    {
        var getNoteResult = await GetByIdAsync(
            id, cancellationToken);

        if (getNoteResult.IsFailure)
        {
            return getNoteResult;
        }

        var note = getNoteResult.Value;

        dbContext.Notes.Remove(note);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<Note>> GetByIdAsync(
        int id, 
        CancellationToken cancellationToken)
    {
        var note = 
            await dbContext.Notes
                .FirstOrDefaultAsync(
                    n => n.Id == id, cancellationToken);

        return note is null
            ? Result<Note>.Failure(
                "The note with this ID was not found.")
            : Result<Note>.Success(note);
    }

    public async Task<PagedResult<Note>> GetPagedAsync(
        NoteQueryParameters queryParameters, 
        CancellationToken cancellationToken)
    {
        IQueryable<Note> notesQuery = 
            dbContext.Notes.AsNoTracking();
        
        notesQuery = ApplyFilters(notesQuery, queryParameters);

        int notesTotalCount = 
            await notesQuery.CountAsync(cancellationToken);

        notesQuery = ApplySorting(notesQuery, queryParameters);

        var pagedNotes = await notesQuery
            .Skip((queryParameters.Page - 1) * queryParameters.PageSize)
            .Take(queryParameters.PageSize)
            .ToListAsync(cancellationToken);

        return new(
            pagedNotes,
            notesTotalCount,
            queryParameters.Page,
            queryParameters.PageSize);
    }

    public async Task<Result<Note>> UpdateDetailsAsync(
        int id, 
        Action<Note> updateAct, 
        CancellationToken cancellationToken)
    {
        var getNoteResult = await GetByIdAsync(id, cancellationToken);

        if (getNoteResult.IsFailure)
        {
            return getNoteResult;
        }

        var note = getNoteResult.Value;
        updateAct(note);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<Note>.Success(note);
    }
}
