using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YouTube.WebApi.Service.DTOs.Videos;
using YouTube.WebApi.Service.Interfaces;

namespace YouTube.WebApi.Controllers
{
    [Route("api/videos")]
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


    }
}
