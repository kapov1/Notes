namespace Notes.Infrastructure.Persistence.Repositories.Notes;

public class NoteRepository(
    AppDbContext dbContext) : INoteRepository
{
    private IQueryable<Note> ApplyFilters(
        IQueryable<Note> notes, 
        NoteQueryParameters queryParameters)
    {
        if (queryParameters.AuthorId is not null)
        {
            notes = notes.Where(
                n => n.AuthorId == queryParameters.AuthorId);
        }
        if (queryParameters.Title is not null)
        {
            notes = notes.Where(
                n => n.Title.Contains(queryParameters.Title));
        }
        if (queryParameters.CreatedFrom is not null)
        {
            notes = notes.Where(
                n => n.CreatedAt >= queryParameters.CreatedFrom);
        }
        if (queryParameters.CreatedTo is not null)
        {
            notes = notes.Where(
                n => n.CreatedAt <= queryParameters.CreatedTo);
        }
        if (queryParameters.Tags is not null && 
            queryParameters.Tags.Any())
        {
            notes = notes.Where(
                n => n.Tags.All(
                    tag => queryParameters.Tags.Contains(tag)));
        }

        return notes;
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
        IQueryable<Note> notes, 
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
            ? notes.OrderBy(firstSortExpression)
            : notes.OrderByDescending(firstSortExpression);
            
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

    public async Task<Result<Note>> CreateAsync(
        Note note, 
        CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.Notes.AddAsync(
                note, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result<Note>.Success(note);
        }
        catch(DbUpdateException)
        {
            return Result<Note>.Failure("Failed to create note.");
        }
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
        IQueryable<Note> notes = 
            dbContext.Notes.AsNoTracking();
        
        notes = ApplyFilters(notes, queryParameters);

        int notesTotalCount = 
            await notes.CountAsync(cancellationToken);

        notes = ApplySorting(notes, queryParameters);

        var pagedNotes = await notes
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
