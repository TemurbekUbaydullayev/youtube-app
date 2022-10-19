using AutoMapper;
using System.Linq.Expressions;
using YouTube.WebApi.Data.DbContexts;
using YouTube.WebApi.Data.Interfaces;
using YouTube.WebApi.Domain.Commons;
using YouTube.WebApi.Domain.Entities;
using YouTube.WebApi.Service.Commons.Exceptions;
using YouTube.WebApi.Service.Commons.Extensions;
using YouTube.WebApi.Service.Commons.Helpers;
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
    private readonly IUserService _userService;

    public VideoService(IFileService fileService,
        AppDbContext appDbContext,
        IMapper mapper,
        IVideoRepository videoRepository,
        IUserRepository userRepository,
        IUserService userService)
    {
        _fileService = fileService;
        _dbSet = appDbContext;
        _mapper = mapper;
        _videRepository = videoRepository;
        _userRepository = userRepository;
        _userService = userService;
    }
    public async Task<bool> CreateAsync(long userId, VideoForCreationDto dto)
    {
        var userCheck = await _userRepository.GetAsync(p => p.Id == userId);
        if (userCheck is null)
            throw new NotFoundException("User");

        var video = _mapper.Map<Video>(dto);
        video.UserId = userId;
        video.VideoPath = await _fileService.SaveVideoAsync(dto.Video);
        video.VideoSize = dto.Video.Length / 1024 / 1024;

        await _videRepository.CreateAsync(video);
        await _dbSet.SaveChangesAsync();

        return true;
    }

    public async Task DeleteAsync(Expression<Func<Video, bool>> expression)
    {
        var video = await _videRepository.GetAsync(expression);
        if (video is null)
            throw new NotFoundException("Video");

        await _videRepository.DeleteAsync(video);
        await _dbSet.SaveChangesAsync();
    }

    public async Task<IEnumerable<VideoForViewDto>> GetAllAsync(Expression<Func<Video, bool>>? expression = null, PaginationParameters? parameters = null)
    {
        var videos = _videRepository.GetAll(expression).Where(p => p.User.IsActive.Equals(true)).ToList();

        var views = new List<VideoForViewDto>();
        foreach (var video in videos)
        {
            var view = _mapper.Map<VideoForViewDto>(video);
            view.Data = video!.CreatedAt.ToString("dd/MM/yyyyy");
            view.Time = video!.CreatedAt.ToString("HH:mm:ss");
            view.VideoUrl = video.VideoPath; // FileHelper.MakeVideoUrl(video.VideoPath);
            view.User = await _userService.GetAsync(p => p.Id == video.UserId);

            views.Add(view);
        }

        return views.ToPagedAsEnumerable(parameters);
    }

    public async Task<VideoForViewDto> GetAsync(long id, Expression<Func<Video, bool>> expression)
    {
        var video = await _videRepository.GetAsync(expression);
        var user = await _userService.GetAsync(p => p.Id == id);
        if (video is null)
            throw new NotFoundException("Video");

        var view = _mapper.Map<VideoForViewDto>(video);
        view.Data = video!.CreatedAt.ToString("dd/MM/yyyyy");
        view.Time = video!.CreatedAt.ToString("HH:mm:ss");
        view.VideoUrl = video.VideoPath; // FileHelper.MakeVideoUrl(video.VideoPath);
        view.User = user;

        return view;
    }

    public async Task<bool> UpdateAsync(long userId, long id, VideoForCreationDto dto)
    {
        var video = await _videRepository.GetAsync(p => p.Id == id);

        if (video is null)
            throw new NotFoundException("Videos");

        var upVideo = _mapper.Map<Video>(dto);
        upVideo.UserId = userId;
        upVideo.Id = id;
        upVideo.UpdatedAt = DateTime.UtcNow;
        upVideo.VideoPath = await _fileService.SaveVideoAsync(dto.Video);

        await _videRepository.UpdateAsync(upVideo);
        await _dbSet.SaveChangesAsync();

        return true;
    }
}
