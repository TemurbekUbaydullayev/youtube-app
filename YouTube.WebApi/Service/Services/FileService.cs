using Serilog;
using YouTube.WebApi.Service.Commons.Helpers;
using YouTube.WebApi.Service.Interfaces;

namespace YouTube.WebApi.Service.Services;

public class FileService : IFileService
{
    private readonly string _basePath = string.Empty;
    private const string _imageFolderName = "images";
    private const string _videoFolderName = "videos";

    public string ImageFolderName => _imageFolderName;

    public FileService(IWebHostEnvironment environment)
    {
        _basePath = environment.WebRootPath;
    }
    public Task<bool> DeleteFileAsync(string relativeFilePath)
    {
        string absoluteFilePath = Path.Combine(_basePath, relativeFilePath);

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
        if (!Directory.Exists(Path.Combine(_basePath, _imageFolderName)))
        {
            Log.Error("images not exist!");
            Directory.CreateDirectory(Path.Combine(_basePath, _imageFolderName));
        }

        string fileName = FileHelper.MakeFileName(image.FileName);
        string partPath = Path.Combine(_imageFolderName, fileName);
        string path = Path.Combine(_basePath, partPath);

        var stream = File.Create(path);
        await image.CopyToAsync(stream);
        stream.Close();

        return partPath;
    }

    public async Task<string> SaveVideoAsync(IFormFile video)
    {
        if (!Directory.Exists(Path.Combine(_basePath, _videoFolderName)))
        {
            Log.Error("videos not exist!");
            Directory.CreateDirectory(Path.Combine(_basePath, _videoFolderName));
        }

        string fileName = FileHelper.MakeFileName(video.FileName);
        string partPath = Path.Combine(_videoFolderName, fileName);
        string path = Path.Combine(_basePath, partPath);

        var stream = File.Create(path);
        await video.CopyToAsync(stream);
        stream.Close();

        return partPath;
    }
}
