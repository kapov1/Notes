namespace Notes.Infrastructure.Persistence.Context;

public class AppDbContext(
    DbContextOptions<AppDbContext> options) 
    : DbContext(options)
{
    public DbSet<Note> Notes => Set<Note>();

    public DbSet<User> Users => Set<User>();

    public DbSet<Tag> Tags => Set<Tag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);
    }
}
