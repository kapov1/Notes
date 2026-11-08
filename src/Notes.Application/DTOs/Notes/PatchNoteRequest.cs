namespace Notes.Application.DTOs.Notes;

public class PatchNoteRequest
{
    [StringLength(100)]
    [EmptyOrWhiteSpaces]
    public string? Title { get; init; }

    [EmptyOrWhiteSpaces]
    public string? Content { get; init; }

    [TagNames]
    public ICollection<string>? TagNames { get; init; }
}
