namespace Notes.Domain.Entities;

public class Tag
{
    public Tag(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name;
    }

    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;
}
