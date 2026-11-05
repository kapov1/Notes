using Notes.Application.Common.Attributes;

namespace Notes.Application.DTOs.Users;

public class UpdateUserRequest
{
    [Required]
    [StringLength(100)]
    [EmptyOrWhiteSpaces]
    public string Name { get; init; } = string.Empty;
}
