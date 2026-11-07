namespace Notes.Application.Models.Notes;

public class NoteQueryParameters
{
    [Range(1, int.MaxValue)]
    public int? AuthorId { get; init; }

    [Range(1, int.MaxValue)]
    public int Page { get; init; } = 1;

    [Range(1, int.MaxValue)]
    public int PageSize { get; init; } = 10;

    [StringLength(100)]
    [EmptyOrWhiteSpaces]
    public string? Title { get; init; }

    [TagNames]
    public ICollection<Tag> Tags { get; init; } = [];

    public DateTimeOffset? CreatedFrom { get; init; }

    public DateTimeOffset? CreatedTo { get; init; }

    public ListSortDirection SortDirection { get; init; } = ListSortDirection.Descending;

    [MinLength(1)]
    public IList<NoteSortBy> SortBy { get; init; } = [NoteSortBy.Date];
}
