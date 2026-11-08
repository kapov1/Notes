using Notes.Application.Services;

namespace Notes.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        return services
            .AddScoped<INoteService, NoteService>()
            .AddScoped<IUserService, UserService>();
    }
}
