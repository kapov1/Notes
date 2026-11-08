namespace Notes.Application.Interfaces.Services;

public interface INoteService
{
    Task<PagedResult<NoteResponse>> GetPagedAsync(
        NoteQueryParameters queryParameters,
        CancellationToken cancellationToken);

    Task<Result<NoteResponse>> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<NoteResponse> CreateAsync(
        CreateNoteRequest createRequest,
        CancellationToken cancellationToken);

    Task<Result<NoteResponse>> UpdateAsync(
        int id,
        UpdateNoteRequest updateRequest,
        CancellationToken cancellationToken);

    Task<Result<NoteResponse>> PatchAsync(
        int id,
        PatchNoteRequest patchRequest,
        CancellationToken cancellationToken);

    Task<Result> DeleteAsync(
        int id,
        CancellationToken cancellationToken);
}
