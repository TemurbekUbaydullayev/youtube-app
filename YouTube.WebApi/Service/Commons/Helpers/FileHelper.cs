namespace YouTube.WebApi.Service.Commons.Helpers;

public class FileHelper
{
    public static string MakeFileName(string fileName)
    {
        string guid = Guid.NewGuid().ToString();
        return "IMG_" + guid + fileName;
    }

    public static string MakeImageUrl(string partPath)
        => "https://you-tube-web-app.herokuapp.com/" + partPath;

    public static string MakeVideoUrl(string partPath)
        => "https://you-tube-web-app.herokuapp.com/" + partPath;
}
