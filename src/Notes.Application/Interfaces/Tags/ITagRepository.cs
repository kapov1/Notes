namespace Notes.Application.Interfaces.Tags;

public interface ITagRepository
{
    Task<ICollection<Tag>> GetByNamesAsync(
        IEnumerable<string> tagNames,
        CancellationToken cancellationToken);

    Task<ICollection<Tag>> CreateRangeAsync( 
        IEnumerable<string> tagNags,
        CancellationToken cancellationToken);
}
