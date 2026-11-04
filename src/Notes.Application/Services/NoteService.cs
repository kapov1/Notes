using Notes.Application.Interfaces;

namespace Notes.Application.Services;

public class NoteService(
    INoteRepository noteRepository) : INoteService
{
    public async Task<Result<Note>> CreateAsync(
        CreateNoteRequest createRequest,
        CancellationToken cancellationToken)
    {
        Note note = new(
            createRequest.Title,
            createRequest.Author,
            createRequest.Content,
            createRequest.Tags
        );

        var createResult = await noteRepository.CreateAsync(
            note,
            cancellationToken);

        return createResult;
    }

    public async Task<Result> DeleteAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await noteRepository.DeleteAsync(
            id,
            cancellationToken);
    }

    public async Task<IEnumerable<Note>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await noteRepository.GetAllAsync(
            cancellationToken);
    }

    public async Task<IEnumerable<Note>> GetAllByAuthorAsync(
        string author,
        CancellationToken cancellationToken)
    {
        return await noteRepository.GetByConditionAsync(
            note => note.Author == author,
            cancellationToken);
    }

    public async Task<IEnumerable<Note>> GetAllByTitleAsync(
        string title,
        CancellationToken cancellationToken)
    {
        return await noteRepository.GetByConditionAsync(
            note => note.Title == title,
            cancellationToken);
    }

    public async Task<Result<Note>> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await noteRepository.GetByIdAsync(
            id,
            cancellationToken);
    }

    public async Task<PagedResult<Note>> GetPagedAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        return await noteRepository.GetPagedAsync(
            page,
            pageSize,
            cancellationToken);
    }

    public async Task<Result<Note>> UpdateAsync(
        UpdateNoteRequest updateRequest,
        CancellationToken cancellationToken)
    {
        return await noteRepository.UpdateAsync(
            updateRequest.Id,
            updateRequest.Title,
            updateRequest.Content,
            updateRequest.Tags,
            cancellationToken);
    }

    public async Task<Result<Note>> UpdateContentAsync(
        int id,
        string content,
        CancellationToken cancellationToken)
    {
        return await noteRepository.UpdateDetailAsync(
            id,
            note => note.UpdateContent(content),
            cancellationToken);
    }

    public async Task<Result<Note>> UpdateTagsAsync(
        int id,
        IEnumerable<Tags> tags,
        CancellationToken cancellationToken)
    {
        return await noteRepository.UpdateDetailAsync(
            id,
            note => note.UpdateTags(tags),
            cancellationToken);
    }

    public async Task<Result<Note>> UpdateTitleAsync(
        int id,
        string title,
        CancellationToken cancellationToken)
    {
        return await noteRepository.UpdateDetailAsync(
            id,
            note => note.UpdateTitle(title),
            cancellationToken);
    }
}
