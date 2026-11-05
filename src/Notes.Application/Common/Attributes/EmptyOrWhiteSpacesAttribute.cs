namespace Notes.Application.Common.Attributes;

public class EmptyOrWhiteSpacesAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string)
        {
            return ValidationResult.Success;
        }

        string valueString = (string)value;

        if (string.IsNullOrWhiteSpace(valueString))
        {
            return new("The cannot be empty or whitespace.");
        }

        return ValidationResult.Success;
    }
}
