namespace YouTube.WebApi.Service.Commons.Helpers
{
    public class HttpContextHelper
    {
        public static IHttpContextAccessor Accessor;
        public static HttpContext Context => Accessor.HttpContext ?? new DefaultHttpContext();
        public static IHeaderDictionary ResponseHeaders => Context.Response?.Headers ?? new HeaderDictionary();

        public static long? UserId => long.Parse(Context?.User?.FindFirst("Id")?.Value ?? "0");
        public static string? Role => Context?.User?.FindFirst("Role")?.Value;
    }
}
