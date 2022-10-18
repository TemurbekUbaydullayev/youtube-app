using AutoMapper;
using System.Linq.Expressions;
using YouTube.WebApi.Data.DbContexts;
using YouTube.WebApi.Data.Interfaces;
using YouTube.WebApi.Domain.Commons;
using YouTube.WebApi.Domain.Entities;
using YouTube.WebApi.Service.Commons.Exceptions;
using YouTube.WebApi.Service.DTOs.Videos;
using YouTube.WebApi.Service.Interfaces;

namespace YouTube.WebApi.Service.Services;

public class VideoService : IVideoService
{
    private readonly IFileService _fileService;
    private readonly AppDbContext _dbSet;
    private readonly IMapper _mapper;
    private readonly IVideoRepository _videRepository;
    private readonly IUserRepository _userRepository;

    public VideoService(IFileService fileService,
        AppDbContext appDbContext,
        IMapper mapper,
        IVideoRepository videoRepository,
        IUserRepository userRepository)
    {
        _fileService = fileService;
        _dbSet = appDbContext;
        _mapper = mapper;
        _videRepository = videoRepository;
        _userRepository = userRepository;
    }
    public async Task<bool> CreateAsync(long userId, VideoForCreationDto dto)
    {
        var userCheck = await _userRepository.GetAsync(p => p.Id == userId);
        if (userCheck is null)
            throw new NotFoundException("User");

        var video = _mapper.Map<Video>(dto);
        video.UserId = userId;

        await _videRepository.CreateAsync(video);
        await _dbSet.SaveChangesAsync();

        return true;
    }

    public Task DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<VideoForViewDto>> GetAllAsync(Expression<Func<Video, bool>>? expression = null, PaginationParameters? parameters = null)
    {
        throw new NotImplementedException();
    }

    public Task<VideoForViewDto> GetAsync(Expression<Func<Video, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(long userId, long id, VideoForCreationDto dto)
    {
        throw new NotImplementedException();
    }
}
