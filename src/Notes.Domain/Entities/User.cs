namespace Notes.Domain.Entities;

public class User
{
    public User(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name;
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    public void UpdateName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name;
    }
}
