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
        public async Task<IActionResult> LoginAsync([FromForm]UserForLoginDto loginDto)
        {
            var result = await _accountService.LoginAsync(loginDto);
            return Ok(new {Token = result});
        }

        [HttpPost("email"), AllowAnonymous]
        public async Task<IActionResult> SendEmailAsync([FromBody]string email)
        {
            var res = await _accountService.SendEmailAsync(email);
            return Ok(res);
        }

        [HttpPost("emailverify"), AllowAnonymous]
        public async Task<IActionResult> VerifyEmailAsync(string email, string code)
        {
            var result = await _accountService.VerifyEmailAsync(email, code);
            return Ok(new {Token = result });
        }

        [HttpPost("confirmedpassword"), Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> CheckConfirmPasswordAsync(string password)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value ?? "";
            var result = await _accountService.CheckConfirmPasswordAsync(userEmail, password);
            return Ok(result);
        }
    }
}
