using System.ComponentModel.DataAnnotations;

namespace YouTube.WebApi.Service.DTOs.Users;

public class UserForCreationDto
{
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string LastName { get; set; } = null!;

    [MaxLength(70)]
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Please enter valid email")]
    public string Email { get; set; } = null!;

    [MaxLength(200)]
    public IFormFile Image { get; set; } = null!;

    [MinLength(4)]
    [MaxLength(8)]
    public string Password { get; set; } = null!;
}
