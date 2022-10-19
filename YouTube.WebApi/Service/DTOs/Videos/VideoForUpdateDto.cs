using System.ComponentModel.DataAnnotations;

namespace YouTube.WebApi.Service.DTOs.Videos;

public class VideoForUpdateDto
{
    [MinLength(1), MaxLength(50)]
    public string Name { get; set; } = null!;
}
