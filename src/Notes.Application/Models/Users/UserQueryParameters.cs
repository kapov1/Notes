namespace Notes.Application.Models.Users;

public class UserQueryParameters
{
    [Range(1, int.MaxValue)]
    public int Page { get; init; } = 1;

    [Range(1, int.MaxValue)]
    public int PageSize { get; init; } = 10;

    [StringLength(100)]
    public string? Name { get; init; } = string.Empty;

    public ListSortDirection SortDirection { get; init; } = ListSortDirection.Ascending;

    public IList<UserSortBy> SortBy { get; init; } = [UserSortBy.Name];
}
