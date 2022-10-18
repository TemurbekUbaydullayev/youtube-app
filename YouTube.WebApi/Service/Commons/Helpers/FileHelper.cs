namespace YouTube.WebApi.Service.Commons.Helpers;

public class FileHelper
{
    public static string MakeFileName(string fileName)
    {
        string guid = Guid.NewGuid().ToString();
        return "IMG_" + guid + fileName;
    }
}
