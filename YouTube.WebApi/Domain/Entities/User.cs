using System.ComponentModel.DataAnnotations;
using YouTube.WebApi.Domain.Commons;
using YouTube.WebApi.Domain.Enums;

namespace YouTube.WebApi.Domain.Entities;

public class User : Auditable
{
    [MinLength(2), MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [MinLength(2), MaxLength(50)]
    public string LastName { get; set; } = null!;

    [MaxLength(70)]
    public string Email { get; set; } = null!;

    public string? ImagePath { get; set; }

    public UserRole UserRole { get; set; } = UserRole.User;

    public string Password { get; set; } = null!;

}
