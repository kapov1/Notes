namespace Notes.Domain.Entities;

public class Note
{
    public Note(
        string title,
        int authorId,
        string content,
        ICollection<Tag> tags)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        if (title.Length > 100)
        {
            throw new ArgumentOutOfRangeException();
        }
        ArgumentException.ThrowIfNullOrWhiteSpace(content);
        if (authorId <= 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        Title = title;
        Content = content;
        AuthorId = authorId;
        Tags = tags;
    }

    public int Id { get; private set; }
    public int AuthorId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public ICollection<Tag> Tags { get; private set; } = [];
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;

    public void UpdateTitle(string title)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        if (title.Length > 100)
        {
            throw new ArgumentOutOfRangeException();
        }

        Title = title;
    }

    public void UpdateContent(string content)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(content);

        Content = content;
    }

    public void UpdateTags(ICollection<Tag> tags)
    {
        foreach (var tag in tags)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(tag.Name);
        }

        Tags = tags;
    }
}
