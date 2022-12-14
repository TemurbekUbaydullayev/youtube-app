using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YouTube.WebApi.Service.DTOs.Users;
using YouTube.WebApi.Service.Interfaces;

namespace YouTube.WebApi.Controllers
{
    [Route("accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("registr"), AllowAnonymous]
        public async Task<IActionResult> RegistrAsync([FromForm]UserForCreationDto dto)
        {
            var res = await _accountService.RegisterAsync(dto);
            return Ok(res);
        }
        [HttpPost("login"), AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] UserForLoginDto loginDto)
        {
            var result = await _accountService.LoginAsync(loginDto);
            return Ok(new {Token = result});
        }

        [HttpPost("email"), AllowAnonymous]
        public async Task<IActionResult> SendEmailAsync([FromForm]UserForEmailSendDto dto)
        {
            var res = await _accountService.SendEmailAsync(dto);
            return Ok(res);
        }

        [HttpPost("emailverify"), AllowAnonymous]
        public async Task<IActionResult> VerifyEmailAsync([FromForm]UserForConfirmPasswordDto dto)
        {
            var result = await _accountService.VerifyEmailAsync(dto);
            return Ok(new {Token = result });
        }

        [HttpPost("confirmedpassword"), Authorize]
        public async Task<IActionResult> CheckConfirmPasswordAsync([FromForm]UserForUpdatePasswordDto dto)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value ?? "";
            var result = await _accountService.CheckConfirmPasswordAsync(userEmail, dto);
            return Ok(result);
        }
    }
}
