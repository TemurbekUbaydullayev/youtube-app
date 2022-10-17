using YouTube.WebApi.Domain.Entities;

namespace YouTube.WebApi.Service.DTOs.Videos;

public class VideoForViewDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string VideoUrl { get; set; } = null!;
    public string VideoDownloadUrl { get; set; } = null!;
    public string Time { get; set; } = null!;
    public string Data { get; set; } = null!;
    public User User { get; set; } = null!;
}
