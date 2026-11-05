namespace Notes.Application.DTOs.Notes;

public class NoteResponse(Note note)
{
    public int Id { get; } = note.Id;

    public int AuthorId { get; } = note.AuthorId;

    public string Title { get; } = note.Title;

    public string Content { get; } = note.Content;

    public IEnumerable<Tags> Tags { get; } = note.Tags;

    public DateTimeOffset CreatedAt { get; } = note.CreatedAt;
}
