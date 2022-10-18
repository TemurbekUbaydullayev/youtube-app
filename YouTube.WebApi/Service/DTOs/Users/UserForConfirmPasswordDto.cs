using System.ComponentModel.DataAnnotations;

namespace YouTube.WebApi.Service.DTOs.Users;

public class UserForConfirmPasswordDto
{
    [MaxLength(70)]
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
    ErrorMessage = "Please enter valid email")]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(4)]
    [MaxLength(4)]
    [RegularExpression(@"^[0-9]", ErrorMessage = "Please enter valid code!")]
    public string Code { get; set; } = null!;
}
