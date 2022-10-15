using System.ComponentModel.DataAnnotations;

namespace YouTube.WebApi.Service.DTOs.Users
{
    public class UserForViewDto
    {
        public uint Id { get; set; }

        [MinLength(2), MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [MinLength(2), MaxLength(50)]
        public string LastName { get; set; } = null!;

        [MaxLength(70)]
        public string Email { get; set; } = null!;

        [MaxLength(200)]
        public string ImageUrl { get; set; } = null!;
    }
}
