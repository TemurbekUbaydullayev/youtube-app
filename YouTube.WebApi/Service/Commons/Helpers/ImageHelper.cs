namespace YouTube.WebApi.Service.Commons.Helpers;

public class ImageHelper
{
    public static string MakeImageName(string fileName)
    {
        string guid = Guid.NewGuid().ToString();
        return "IMG_" + guid;
    }
}
