namespace Notes.Application.Services;

public class NoteService(
    INoteRepository noteRepository,
    ITagRepository tagRepository) : INoteService
{
    private async Task<ICollection<Tag>> ResolveTagsAsync(
        IEnumerable<string> tagNames,
        CancellationToken cancellationToken)
    {
        var tagNamesLower = tagNames.Select(name => name.ToLower()).ToList();

        var existingTags = await tagRepository.GetByNamesAsync(
            tagNamesLower,
            cancellationToken);

        var existingTagNames = existingTags
            .Select(t => t.Name.ToLower())
            .ToHashSet();
    
        var missingTagNames = tagNamesLower
            .Where(tag => !existingTagNames.Contains(tag))
            .ToList();

        var newTags = await tagRepository.CreateRangeAsync(
            missingTagNames, 
            cancellationToken);

        var allTags = existingTags
            .Concat(newTags)
            .ToList();

        return allTags;
    }

    public async Task<NoteResponse> CreateAsync(
        CreateNoteRequest createRequest,
        CancellationToken cancellationToken)
    {
        var tags = await ResolveTagsAsync(
            createRequest.Tags, 
            cancellationToken);

        var createdNote = await noteRepository.CreateAsync(
            new(
                createRequest.Title,
                createRequest.AuthorId,
                createRequest.Content,
                tags),
                cancellationToken);

        NoteResponse noteResponse = new(createdNote);

        return noteResponse;
    }

    public async Task<Result> DeleteAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await noteRepository.DeleteAsync(
            id,
            cancellationToken);
    }

    public async Task<Result<NoteResponse>> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var getNoteResult = await noteRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (getNoteResult.IsFailure)
        {
            return Result<NoteResponse>.Failure(
                getNoteResult.ErrorMessage);
        }

        NoteResponse noteResponse = new(getNoteResult.Value);

        return Result<NoteResponse>.Success(noteResponse);
    }

    public async Task<PagedResult<NoteResponse>> GetPagedAsync(
        NoteQueryParameters queryParameters,
        CancellationToken cancellationToken)
    {
        var pagedNotes = await noteRepository.GetPagedAsync(
            queryParameters,
            cancellationToken);

        var notesResponses = pagedNotes.Items.Select(
            note => new NoteResponse(note));

        return new(
            notesResponses,
            pagedNotes.TotalCount,
            pagedNotes.Page,
            pagedNotes.PageSize);
    }

    public async Task<Result<NoteResponse>> UpdateAsync(
        int id,
        UpdateNoteRequest updateRequest,
        CancellationToken cancellationToken)
    {
        var updateNoteResult = await noteRepository.UpdateDetailsAsync(
            id,
            note =>
            {   note.UpdateTitle(updateRequest.Title);
                note.UpdateContent(updateRequest.Content);
                note.UpdateTags(updateRequest.Tags);
            },
            cancellationToken);

        if (updateNoteResult.IsFailure)
        {
            return Result<NoteResponse>.Failure(
                updateNoteResult.ErrorMessage);
        }

        var updatedNote = updateNoteResult.Value;

        return Result<NoteResponse>.Success(
            new NoteResponse(updatedNote));
    }

    public async Task<Result<NoteResponse>> PatchAsync(
        int id,
        PatchNoteRequest patchRequest,
        CancellationToken cancellationToken)
    {
        var updateNoteResult = await noteRepository.UpdateDetailsAsync(
            id,
            note =>
            {   
                if (patchRequest.Title is not null)
                {
                    note.UpdateTitle(
                        patchRequest.Title);
                }

                if (patchRequest.Content is not null)
                {
                    note.UpdateContent(
                        patchRequest.Content);
                }

                if (patchRequest.Tags is not null)
                {
                    note.UpdateTags(
                        patchRequest.Tags);
                }   
            },
            cancellationToken);

        if (updateNoteResult.IsFailure)
        {
            return Result<NoteResponse>.Failure(
                updateNoteResult.ErrorMessage);
        }

        var updatedNote = updateNoteResult.Value;

        return Result<NoteResponse>.Success(
            new NoteResponse(updatedNote));
    }
}
