using System.ComponentModel.DataAnnotations;

namespace YouTube.WebApi.Service.DTOs.Users;

public class UserForUpdatePasswordDto
{
    [MinLength(4)]
    [MaxLength(8)]
    public string Password { get; set; } = null!;
}
