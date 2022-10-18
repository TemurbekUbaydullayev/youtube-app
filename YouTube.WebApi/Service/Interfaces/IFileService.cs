namespace YouTube.WebApi.Service.Interfaces;

public interface IFileService
{
    public string ImageFolderName { get; }
    Task<string> SaveImageAsync(IFormFile image);
    Task<bool> DeleteFileAsync(string relativeImagePath);

    Task<string> SaveVideoAsync(IFormFile video);
}
