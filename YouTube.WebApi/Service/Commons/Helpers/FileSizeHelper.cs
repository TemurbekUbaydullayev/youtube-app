namespace YouTube.WebApi.Service.Commons.Helpers;

public class FileSizeHelper
{
    public static double ByteToMb(double @byte)
    {
        return @byte / 1024 / 1024;
    }
}
