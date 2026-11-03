namespace Notes.Domain.Interfaces;

public interface INoteService
{
    Task<IEnumerable<Note>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<IEnumerable<Note>> GetAllByTitleAsync(
        string title, 
        CancellationToken cancellationToken);
        
    Task<IEnumerable<Note>> GetAllByAuthorAsync(
        string author, 
        CancellationToken cancellationToken);

    Task<Result<Note>> GetByIdAsync(
        int id, 
        CancellationToken cancellationToken);

    Task<Result<Note>> CreateAsync(
        string title,
        string author,
        string content,
        IEnumerable<Tags> tags,
        CancellationToken cancellationToken);

    Task<Result<Note>> UpdateAsync(
        int id,
        string title,
        string content,
        IEnumerable<Tags> tags,
        CancellationToken cancellationToken);

    Task<Result<Note>> UpdateTitleAsync(
        int id,
        string title,
        CancellationToken cancellationToken);

    Task<Result<Note>> UpdateContentAsync(
        int id, 
        string content,
        CancellationToken cancellationToken);

    Task<Result<Note>> UpdateTagsAsync(
        int id, 
        IEnumerable<Tags> tags,
        CancellationToken cancellationToken);

    Task<Result> DeleteAsync(
        int id, 
        CancellationToken cancellationToken);
}
