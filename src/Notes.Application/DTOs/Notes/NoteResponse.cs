namespace Notes.Application.DTOs.Notes;

public class NoteResponse(Note note)
{
    public int Id { get; } = note.Id;

    public int AuthorId { get; } = note.AuthorId;

    public string Title { get; } = note.Title;

    public string Content { get; } = note.Content;

    public string[] TagNames { get; } = note.Tags.Select(tag => tag.Name).ToArray();

    public DateTimeOffset CreatedAt { get; } = note.CreatedAt;
}
