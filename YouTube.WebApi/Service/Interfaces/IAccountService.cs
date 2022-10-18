using YouTube.WebApi.Service.DTOs.Users;

namespace YouTube.WebApi.Service.Interfaces;

public interface IAccountService
{
    Task<bool> RegisterAsync(UserForCreationDto dto);
    Task<string> LoginAsync(UserForLoginDto dto);
    Task<bool> SendEmailAsync(UserForEmailSendDto email);
    Task<string> VerifyEmailAsync(UserForConfirmPasswordDto dto);
    Task<bool> CheckConfirmPasswordAsync(string email, UserForUpdatePasswordDto dto);
}
