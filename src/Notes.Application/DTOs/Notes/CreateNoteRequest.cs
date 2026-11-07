using Notes.Application.Common.Attributes;

namespace Notes.Application.DTOs.Notes;

public class CreateNoteRequest
{
    [Required]
    [Range(1, int.MaxValue)]
    public int AuthorId { get; init; }

    [Required]
    [StringLength(100)]
    [EmptyOrWhiteSpaces]
    public string Title { get; init; } = string.Empty;

    [Required]
    [EmptyOrWhiteSpaces]
    public string Content { get; init; } = string.Empty;

    [Required] //but can by empty
    [TagNames]
    public ICollection<string> Tags { get; init; } = [];
}
