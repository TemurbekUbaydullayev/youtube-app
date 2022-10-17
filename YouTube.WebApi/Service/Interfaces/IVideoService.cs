using System.Linq.Expressions;
using YouTube.WebApi.Domain.Commons;
using YouTube.WebApi.Domain.Entities;
using YouTube.WebApi.Service.DTOs.Videos;

namespace YouTube.WebApi.Service.Interfaces;

public interface IVideoService
{
    Task<bool> CreateAsync(long userId, VideoForCreationDto dto);
    Task<bool> UpdateAsync(long userId, long id, VideoForCreationDto dto);
    Task DeleteAsync(long id);
    Task<VideoForViewDto> GetAsync(Expression<Func<Video, bool>> expression);
    Task<IEnumerable<VideoForViewDto>> GetAllAsync(Expression<Func<Video, bool>>? expression = null, 
        PaginationParameters? parameters = null);
}
