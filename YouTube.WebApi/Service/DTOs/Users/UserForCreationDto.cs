using System.ComponentModel.DataAnnotations;
using YouTube.WebApi.Service.Commons.Attributes;

namespace YouTube.WebApi.Service.DTOs.Users;

public class UserForCreationDto
{
    [Required(ErrorMessage = "First name is required")]
    [MaxLength(50), MinLength(2)]
    [RegularExpression(@"^(?=.{1,40}$)[a-zA-Z]+(?:[-'\s][a-zA-Z]+)*$",
                ErrorMessage = "Please enter valid first name. " +
                "First name must be contains only letters or ' character")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Last name is required")]
    [MaxLength(50), MinLength(2)]
    [RegularExpression(@"^(?=.{1,40}$)[a-zA-Z]+(?:[-'\s][a-zA-Z]+)*$",
        ErrorMessage = "Please enter valid last name. " +
        "Last name must be contains only letters or ' character")]
    public string LastName { get; set; } = null!;

    [MaxLength(70)]
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Please enter valid email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Image is required!")]
    [DataType(DataType.Upload)]
    [MaxFileSize(3)]
    [AllowedFileExtensionsAttribute(new string[] { ".jpg", ".png", ".jpeg" })]
    public IFormFile Image { get; set; } = null!;

    [MinLength(4)]
    [MaxLength(8)]
    public string Password { get; set; } = null!;
}
