namespace Notes.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string connection = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Database connection string 'DefaultConnection' is not configured.");

        return services
            .AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connection);
            })
            .AddScoped<INoteRepository, NoteRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<ITagRepository, TagRepository>();
    }
}
