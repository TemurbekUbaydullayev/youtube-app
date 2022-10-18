using YouTube.WebApi.Domain.Entities;
using YouTube.WebApi.Service.DTOs.Users;

namespace YouTube.WebApi.Service.DTOs.Videos;

public class VideoForViewDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string VideoUrl { get; set; } = null!;
    public string VideoDownloadUrl { get; set; } = null!;
    public string Time { get; set; } = null!;
    public string Data { get; set; } = null!;
    public UserForViewDto User { get; set; } = null!;
}
