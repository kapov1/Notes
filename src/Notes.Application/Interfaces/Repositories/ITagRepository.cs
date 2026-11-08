namespace Notes.Application.Interfaces.Repositories;

public interface ITagRepository
{
    Task<ICollection<Tag>> GetByNamesAsync(
        IEnumerable<string> tagNames,
        CancellationToken cancellationToken);

    Task<ICollection<Tag>> CreateRangeAsync( 
        IEnumerable<string> tagNags,
        CancellationToken cancellationToken);
}
