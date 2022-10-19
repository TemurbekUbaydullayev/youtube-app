using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YouTube.WebApi.Domain.Commons;
using YouTube.WebApi.Service.DTOs.Users;
using YouTube.WebApi.Service.Interfaces;

namespace YouTube.WebApi.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var res = await _userService.GetAsync(userId => userId.Id == id);
            return Ok(res);
        }

        [HttpGet("name")]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            var res = await _userService.GetAsync(userName => userName.FirstName.Equals(name));
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery]PaginationParameters @params)
        {
            var res = await _userService.GetAllAsync(expression:null, @params);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            await _userService.DisableAsync(p => p.Id == id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm]UserForCreationDto dto)
        {
            var id = long.Parse (HttpContext.User.FindFirst("Id")?.Value ?? "0");
            var res = await _userService.UpdateAsync(id, dto);
            return Ok(res);
        }
    }
}
