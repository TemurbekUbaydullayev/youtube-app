using System.Linq.Expressions;
using YouTube.WebApi.Domain.Entities;

namespace YouTube.WebApi.Data.Interfaces;

public interface IVideoRepository
{
    Task<Video> CreateAsync(Video video);
    Task<Video> UpdateAsync(Video video);
    Task DeleteAsync(Video video);
    Task<Video?> GetAsync(Expression<Func<Video, bool>> expression);
    IQueryable<Video> GetAll(Expression<Func<Video, bool>>? expression = null, bool isTracking = true);
}
