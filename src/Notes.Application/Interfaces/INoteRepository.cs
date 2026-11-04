using System.Data.Common;

namespace Notes.Application.Interfaces;

public interface INoteRepository
{
    Task<IEnumerable<Note>> GetAllAsync(
        CancellationToken cancellationToken
    );

    Task<PagedResult<Note>> GetPagedAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken);

    Task<IEnumerable<Note>> GetByConditionAsync(
        Expression<Func<Note, bool>> condition,
        CancellationToken cancellationToken);

    Task<Result<Note>> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<Result<Note>> CreateAsync(
        Note note,
        CancellationToken cancellationToken);

    Task<Result<Note>> UpdateAsync(
        int id,
        string title,
        string content,
        IEnumerable<Tags> tags,
        CancellationToken cancellationToken);

    Task<Result<Note>> UpdateDetailAsync(
        int id,
        Action<Note> updateAct,
        CancellationToken cancellationToken);

    Task<Result> DeleteAsync(
        int id,
        CancellationToken cancellationToken);
}
