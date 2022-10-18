using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YouTube.WebApi.Data.DbContexts;
using YouTube.WebApi.Data.Interfaces;
using YouTube.WebApi.Domain.Entities;

namespace YouTube.WebApi.Data.Repositories;

public class VideoRepository : IVideoRepository
{
    private readonly AppDbContext _dbSet;

    public VideoRepository(AppDbContext appDbContext)
    {
        _dbSet = appDbContext;
    }
    public async Task<Video> CreateAsync(Video video)
    {
        var entity = await _dbSet.Videos.AddAsync(video);
        return entity.Entity;
    }

    public Task DeleteAsync(Video video)
    {
        _dbSet.Videos.Remove(video);
        return Task.CompletedTask;
    }

    public IQueryable<Video> GetAll(Expression<Func<Video, bool>>? expression = null, bool isTracking = true)
    {
        var entites = expression is null ? _dbSet.Videos : _dbSet.Videos.Where(expression);

        return isTracking ? entites : entites.AsNoTracking();
    }

    public async Task<Video?> GetAsync(Expression<Func<Video, bool>> expression)
    {
        var res = await _dbSet.Videos.FindAsync(expression);
        return res;
    }

    public async Task<Video> UpdateAsync(Video video)
    {
        var res =_dbSet.Update(video);
        return res.Entity;
    }
}
