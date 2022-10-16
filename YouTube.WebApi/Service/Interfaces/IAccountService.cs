using YouTube.WebApi.Service.DTOs.Users;

namespace YouTube.WebApi.Service.Interfaces;

public interface IAccountService
{
    Task<bool> RegisterAsync(UserForCreationDto dto);
    Task<string> LoginAsync(UserForLoginDto dto);
    Task<bool> SendEmailAsync(string email);
    Task<string> VerifyEmailAsync(string email, string code);
    Task<bool> CheckConfirmPasswordAsync(string email, string password);
}
