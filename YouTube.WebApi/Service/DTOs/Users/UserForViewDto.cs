using System.ComponentModel.DataAnnotations;

namespace YouTube.WebApi.Service.DTOs.Users
{
    public class UserForViewDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}
