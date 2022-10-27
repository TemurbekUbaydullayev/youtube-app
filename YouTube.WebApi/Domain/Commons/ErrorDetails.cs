namespace YouTube.WebApi.Domain.Commons;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
}
