namespace Notes.Application.Interfaces.Repositories;

public interface INoteRepository
{
    Task<PagedResult<Note>> GetPagedAsync(
        NoteQueryParameters queryParameters,
        CancellationToken cancellationToken);

    Task<Result<Note>> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<Result<Note>> CreateAsync(
        Note note,
        CancellationToken cancellationToken);

    Task<Result<Note>> UpdateDetailsAsync(
        int id,
        Action<Note> updateAct,
        CancellationToken cancellationToken);

    Task<Result> DeleteAsync(
        int id,
        CancellationToken cancellationToken);
}
