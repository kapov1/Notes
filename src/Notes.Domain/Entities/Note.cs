namespace Notes.Domain.Entities;

public class Note
{
    public int Id { get; set; } 
    public string Title { get; set; } = string.Empty;   
    public string Author { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public IEnumerable<Tags> Tags { get; set; } = [];
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
}
