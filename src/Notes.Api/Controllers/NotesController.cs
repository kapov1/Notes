namespace Notes.Api.Controllers;

[ApiController]
[Route("api/notes")]
public class NotesController(
    INoteService noteService) : ControllerBase
{
    [HttpGet]
    public async Task<Ok<PagedResult<NoteResponse>>> GetPagedAsync(
        [FromQuery] NoteQueryParameters queryParameters,
        CancellationToken cancellationToken)
    {
        var pagedNotes =  await noteService.GetPagedAsync(
            queryParameters, 
            cancellationToken);

        return TypedResults.Ok(pagedNotes);
    }

    [HttpGet("{id:int}", Name = "GetNoteById")]
    public async Task<Results<Ok<NoteResponse>, NotFound<string>>> GetByIdAsync(
        [Required, Range(1, int.MaxValue)] int id, 
        CancellationToken cancellationToken)
    {
        var getResult = await noteService.GetByIdAsync(
            id, 
            cancellationToken);
            
        return getResult.IsSuccess
            ? TypedResults.Ok(getResult.Value)
            : TypedResults.NotFound(getResult.ErrorMessage);
    }

    [HttpPost]
    public async Task<CreatedAtRoute<NoteResponse>> CreateAsync(
        [FromBody] CreateNoteRequest createRequest,
        CancellationToken cancellationToken)
    {
        var createdNote = await noteService.CreateAsync(
            createRequest, 
            cancellationToken);

        return TypedResults.CreatedAtRoute(
                createdNote,
                "GetNoteById",
                new { id = createdNote.Id });
    }

    [HttpPut("{id:int}")]
    public async Task<Results<Ok<NoteResponse>, NotFound<string>>> UpdateAsync(
        [Required, Range(1, int.MaxValue)] int id, 
        [FromBody] UpdateNoteRequest updateRequest,
        CancellationToken cancellationToken)
    {
        var updateNoteResult = await noteService.UpdateAsync(
            id, 
            updateRequest, 
            cancellationToken);

        return updateNoteResult.IsSuccess 
            ? TypedResults.Ok(updateNoteResult.Value)
            : TypedResults.NotFound(updateNoteResult.ErrorMessage);
    }

    [HttpPatch("{id:int}")]
    public async Task<Results<Ok<NoteResponse>, NotFound<string>>> PatchAsync(
        [Required, Range(1, int.MaxValue)] int id, 
        [FromBody] PatchNoteRequest patchRequest,
        CancellationToken cancellationToken)
    {
        var patchNoteResult = await noteService.PatchAsync(
            id, 
            patchRequest, 
            cancellationToken);

        return patchNoteResult.IsSuccess 
            ? TypedResults.Ok(patchNoteResult.Value)
            : TypedResults.NotFound(patchNoteResult.ErrorMessage);
    }

    [HttpDelete("{id:int}")]
    public async Task<Results<NoContent, NotFound<string>>> DeleteAsync(
        [Required, Range(1, int.MaxValue)] int id, 
        CancellationToken cancellationToken)
    {
        var deleteNoteResult = await noteService.DeleteAsync(
            id, 
            cancellationToken);

        return deleteNoteResult.IsSuccess 
            ? TypedResults.NoContent()
            : TypedResults.NotFound(deleteNoteResult.ErrorMessage);
    }
}
