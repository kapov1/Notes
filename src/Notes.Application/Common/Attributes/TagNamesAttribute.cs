namespace Notes.Application.Common.Attributes;

public class TagNamesAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not IEnumerable<string>)
        {
            return ValidationResult.Success;
        }

        var tagNames = (IEnumerable<string>)value;

        if (!tagNames.Any())
        {
            return ValidationResult.Success;
        }

        foreach (var tagName in tagNames)
        {
            if (string.IsNullOrWhiteSpace(tagName))
            {
                return new("The tag cannot be empty or whitespace.");
            }
            else if (tagName.Length > 100)
            {
                return new("The tag name must not exceed 100 characters.");
            }
        }

        return ValidationResult.Success;
    }
}
