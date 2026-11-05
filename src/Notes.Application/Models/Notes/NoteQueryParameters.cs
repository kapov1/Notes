namespace Notes.Application.Models.Notes;

public class NoteQueryParameters
{
    [Range(1, int.MaxValue)]
    public int AuthorId { get; init; }

    [Range(1, int.MaxValue)]
    public int Page { get; init; } = 1;

    [Range(1, int.MaxValue)]
    public int PageSize { get; init; } = 10;

    public string? Title { get; init; }

    public List<Tags> Tags { get; init; } = [];

    public DateTimeOffset? CreatedFrom { get; init; }

    public DateTimeOffset? CreatedTo { get; init; }

    public ListSortDirection? SortDirection { get; init; }
}
