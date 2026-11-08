namespace Notes.Infrastructure.Persistence.Repositories;

public class TagRepository(
    AppDbContext dbContext) : ITagRepository
{
    public async Task<ICollection<Tag>> CreateRangeAsync(
        IEnumerable<string> tagNames, 
        CancellationToken cancellationToken)
    {
        var tags = tagNames
            .Select(t => new Tag(t))
            .ToList();

        await dbContext.AddRangeAsync(
            tags, 
            cancellationToken);

        await dbContext.SaveChangesAsync(
            cancellationToken);

        return tags;
    }

    public async Task<ICollection<Tag>> GetByNamesAsync(
        IEnumerable<string> tagNames, 
        CancellationToken cancellationToken)
    {
        ICollection<Tag> tags = await dbContext.Tags
            .Where(t => tagNames.Contains(t.Name))
            .ToListAsync(cancellationToken);

        return tags;
    }
}
