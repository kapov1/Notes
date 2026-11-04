namespace Notes.Application.Common.DTOs;

public class UpdateNoteRequest
{
    [Required]
    [Range(1, int.MaxValue)]
    public int Id { get; init; }

    [Required]
    [StringLength(100)]
    public string Title { get; init; } = string.Empty;

    [Required]
    public string Content { get; init; } = string.Empty;

    public IEnumerable<Tags> Tags { get; init; } = [];
}
