using System.ComponentModel.DataAnnotations;
using YouTube.WebApi.Service.Commons.Attributes;

namespace YouTube.WebApi.Service.DTOs.Videos;

public class VideoForCreationDto
{
    [MinLength(1), MaxLength(50)]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Vide is required!")]
    [AllowedFileExtensions(new string[] {".mp4", ".mov", ".mkv", ".wmv", ".avi" })]
    [MaxFileSize(10)]
    public IFormFile Video { get; set; } = null!;
}
