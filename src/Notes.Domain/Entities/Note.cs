namespace Notes.Domain.Entities;

public class Note
{
    public Note(
        string title,
        int authorId,
        string content,
        IEnumerable<Tag> tags)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(content);
        if (Id <= 0) throw new ArgumentException();

        Title = title;
        AuthorId = authorId;
        Tags = tags;
    }

    public int Id { get; private set; }
    public int AuthorId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public IEnumerable<Tag> Tags { get; private set; } = [];
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;

    public void UpdateTitle(string title)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);

        Title = title;
    }

    public void UpdateContent(string content)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(content);

        Content = content;
    }

    public void UpdateTags(IEnumerable<Tag> tags) => Tags = tags;
}
