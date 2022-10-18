using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YouTube.WebApi.Domain.Commons;
using YouTube.WebApi.Service.DTOs.Videos;
using YouTube.WebApi.Service.Interfaces;

namespace YouTube.WebApi.Controllers
{
    [Route("videos")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly IVideoService _videoService;

        public VideosController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> CreateAsync([FromForm] VideoForCreationDto dto)
        {
            var userId = long.Parse(HttpContext.User.FindFirst("Id")?.Value ?? "0");
            var res = await _videoService.CreateAsync(userId, dto);

            return Ok(res);
        }

        [HttpGet("{id}"), Authorize]
        public async Task<IActionResult> GetByIndexAsync(long id)
        {
            var userId = long.Parse(HttpContext.User.FindFirst("Id")?.Value ?? "0");

            var res = await _videoService.GetAsync(userId, p => p.Id == id);
            return Ok(res);
        }
        [HttpGet, Authorize]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParameters parameters)
        {
            return await _videoService.GetAllAsync(parameters: parameters);
        }
    }
}
