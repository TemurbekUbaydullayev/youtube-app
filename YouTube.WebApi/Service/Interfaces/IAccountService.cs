using YouTube.WebApi.Service.DTOs.Users;

namespace YouTube.WebApi.Service.Interfaces;

public interface IAccountService
{
    Task<string> RegisterAsync(UserForCreationDto dto);
    Task<string> LoginAsync(UserForLoginDto dto);
    Task<bool> SendEmailAsync(string email);
    Task<bool> VerifyEmailAsync(string email, string code);
}
