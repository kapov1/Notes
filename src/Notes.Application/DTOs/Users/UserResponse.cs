namespace Notes.Application.DTOs.Users;

public class UserResponse(User user)
{
    public int Id { get; } = user.Id;

    public string Name { get; } = user.Name;
}
