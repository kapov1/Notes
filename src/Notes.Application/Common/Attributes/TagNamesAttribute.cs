namespace Notes.Application.Common.Attributes;

public class TagNamesAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not IEnumerable<string>)
        {
            return ValidationResult.Success;
        }

        var tags = (IEnumerable<string>)value;

        foreach (var tag in tags)
        {
            if (string.IsNullOrWhiteSpace(tag))
            {
                return new("Tag cannot be empty or whitespace");
            }
        }

        return ValidationResult.Success;
    }
}
