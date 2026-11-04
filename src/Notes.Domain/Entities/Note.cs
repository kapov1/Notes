namespace Notes.Domain.Entities;

public class Note
{
    public Note(
        string title,
        string author,
        string content,
        IEnumerable<Tags> tags)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(author);
        ArgumentException.ThrowIfNullOrWhiteSpace(content);

        Title = title;
        Author = author;
        Tags = tags;
    }

    public int Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Author { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public IEnumerable<Tags> Tags { get; private set; } = [];
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

    public void UpdateTags(IEnumerable<Tags> tags) => Tags = tags;
}
