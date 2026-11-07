namespace Notes.Application.DTOs.Notes;

public class PatchNoteRequest
{
    [StringLength(100)]
    [EmptyOrWhiteSpaces]
    public string? Title { get; init; } = string.Empty;

    [EmptyOrWhiteSpaces]
    public string? Content { get; init; } = string.Empty;

    [TagNames]
    public ICollection<Tag>? Tags { get; init; } = [];
}
