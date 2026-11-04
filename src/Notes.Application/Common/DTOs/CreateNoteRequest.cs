namespace Notes.Application.Common.DTOs;

public class CreateNoteRequest
{
    [Required]
    [StringLength(100)]
    public string Author { get; init; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Title { get; init; } = string.Empty;

    [Required]
    public string Content { get; init; } = string.Empty;

    public IEnumerable<Tags> Tags { get; init; } = [];
}
