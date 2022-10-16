using Serilog;
using YouTube.WebApi.Service.Commons.Helpers;
using YouTube.WebApi.Service.Interfaces;

namespace YouTube.WebApi.Service.Services;

public class FileService : IFileService
{
    private readonly string _basePath = string.Empty;
    private const string _folderName = "images";

    public string ImageFolderName => _folderName;

    public FileService(IWebHostEnvironment environment)
    {
        _basePath = environment.WebRootPath;

        if (!Directory.Exists(Path.Combine(_basePath, _folderName)))
        {
            Log.Error("images not exist!");
            Directory.CreateDirectory(Path.Combine(_basePath, _folderName));
        }
    }
    public Task<bool> DeleteImageAsync(string relativeImagePath)
    {
        string absoluteFilePath = Path.Combine(_basePath, relativeImagePath);

        if (!File.Exists(absoluteFilePath)) return Task.FromResult(false);

        try
        {
            File.Delete(absoluteFilePath);
            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }

    public async Task<string> SaveImageAsync(IFormFile image)
    {
        string fileName = ImageHelper.MakeImageName(image.FileName);
        string partPath = Path.Combine(_folderName, fileName);
        string path = Path.Combine(_basePath, partPath);

        var stream = File.Create(path);
        await image.CopyToAsync(stream);
        stream.Close();

        return partPath;
    }
}
