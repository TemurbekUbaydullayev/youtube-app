using AutoMapper;
using System.Linq.Expressions;
using YouTube.WebApi.Data.DbContexts;
using YouTube.WebApi.Data.Interfaces;
using YouTube.WebApi.Domain.Commons;
using YouTube.WebApi.Domain.Entities;
using YouTube.WebApi.Service.Commons.Exceptions;
using YouTube.WebApi.Service.Commons.Extensions;
using YouTube.WebApi.Service.Commons.Helpers;
using YouTube.WebApi.Service.Commons.Security;
using YouTube.WebApi.Service.DTOs.Users;
using YouTube.WebApi.Service.Interfaces;

namespace YouTube.WebApi.Service.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _dbSet;
    private readonly IFileService _fileService;
    private readonly IUserRepository _userRepository;

    public UserService(IMapper mapper,
        AppDbContext appDbContext,
        IFileService fileService,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _dbSet = appDbContext;
        _fileService = fileService;
        _userRepository = userRepository;
    }
    public async Task DisableAsync(Expression<Func<User, bool>> expression)
    {
        var user = await _userRepository.GetAsync(expression);

        if (user is null)
            throw new NotFoundException("User");

        user.IsActive = false;
        await _userRepository.UpdateAsync(user);
        await _dbSet.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserForViewDto>> GetAllAsync(Expression<Func<User, bool>>? expression = null, PaginationParameters? parameters = null)
    {
        var users = _userRepository.GetAll(expression).Where(p => p.IsActive.Equals(true)).ToPagedAsQueryable(parameters);

        var entities = new List<UserForViewDto>();
        foreach (var user in users)
        {
            var entity = _mapper.Map<UserForViewDto>(user);
            entity.ImageUrl = user.ImagePath!; // FileHelper.MakeImageUrl(user.ImagePath!);
            entities.Add(entity);
        }

        return entities;
    }

    public async Task<UserForViewDto> GetAsync(Expression<Func<User, bool>> expression)
    {
        var user = await _userRepository.GetAsync(expression);
        if (user is null || user.IsActive.Equals(false))
            throw new NotFoundException("User");
        var res = _mapper.Map<UserForViewDto>(user);
        res.ImageUrl = user.ImagePath!; // FileHelper.MakeImageUrl(user.ImagePath!);

        return res; 
    }

    public async Task<UserForViewDto> UpdateAsync(long userId, UserForCreationDto dto)
    {
        var user = await _userRepository.GetAsync(p => p.Id.Equals(userId));

        if (user is null)
            throw new NotFoundException("User");

        await _fileService.DeleteFileAsync(user.ImagePath);
        var imagePath = await _fileService.SaveImageAsync(dto.Image);
        var hashPassword = PasswordHasher.Hash(dto.Password);

        var entity = _mapper.Map<User>(dto);
        entity.Password = hashPassword;
        entity.ImagePath = imagePath;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.Id = userId;

        await _userRepository.UpdateAsync(entity);
        await _dbSet.SaveChangesAsync();

        var res = _mapper.Map<UserForViewDto>(entity);
        res.ImageUrl = entity.ImagePath!; // FileHelper.MakeImageUrl(user.ImagePath!);

        return res;
    }
}
