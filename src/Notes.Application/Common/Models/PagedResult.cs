namespace Notes.Application.Common.Models;

public class PagedResult<T>
{
    public PagedResult(
        IEnumerable<T> items,
        int totalCount,
        int page,
        int pageSize)
    {
        if (totalCount < 0 ||
            page <= 0 ||
            pageSize <= 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        TotalCount = totalCount;
        Page = page;
        PageSize = pageSize;
        Items = items;
    }

    public IEnumerable<T> Items { get; init; } = [];
    
    public int TotalCount { get; init; }

    public int Page { get; init; }

    public int PageSize { get; init; }

    public int TotalPages => 
        (int)Math.Ceiling((double)TotalCount / PageSize);

    public bool IsEmpty => !Items.Any();
}
