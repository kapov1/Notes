using Notes.Application.Interfaces.Users;
using Notes.Infrastructure.Persistence.Repositories.Notes;
using Notes.Infrastructure.Persistence.Repositories.Users;

namespace Notes.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string connection = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Database connection string 'DeafaultConnection' is not configured.");

        return services
            .AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connection);
            })
            .AddScoped<INoteRepository, NoteRepository>()
            .AddScoped<IUserRepository, UserRepository>();
    }
}
