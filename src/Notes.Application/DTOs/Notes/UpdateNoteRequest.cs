namespace Notes.Application.DTOs.Notes;

public class UpdateNoteRequest
{
    [Required]
    [StringLength(100)]
    [EmptyOrWhiteSpaces]
    public string Title { get; init; } = string.Empty;

    [Required]
    [EmptyOrWhiteSpaces]
    public string Content { get; init; } = string.Empty;

    [Required] //but can by empty
    [TagNames]
    public IEnumerable<Tag> Tags { get; init; } = [];
}
