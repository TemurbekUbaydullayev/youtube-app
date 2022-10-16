namespace YouTube.WebApi.Service.Interfaces;

public interface IEmailService
{
    Task SendAsync(string email, string message);
}
