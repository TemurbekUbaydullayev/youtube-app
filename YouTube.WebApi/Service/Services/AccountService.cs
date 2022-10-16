using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using YouTube.WebApi.Data.DbContexts;
using YouTube.WebApi.Data.Interfaces;
using YouTube.WebApi.Domain.Entities;
using YouTube.WebApi.Service.Commons.Exceptions;
using YouTube.WebApi.Service.Commons.Security;
using YouTube.WebApi.Service.DTOs.Users;
using YouTube.WebApi.Service.Interfaces;

namespace YouTube.WebApi.Service.Services;

public class AccountService : IAccountService
{
    private readonly IConfigurationSection _config;
    private readonly IUserRepository _userRepository;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    private readonly AppDbContext _dbSet;
    private readonly IEmailService _emailService;

    public AccountService(IConfiguration configuration,
        IUserRepository userRepository,
        IFileService fileService,
        IMapper mapper,
        IMemoryCache memoryCache,
        AppDbContext appDbContext,
        IEmailService emailService)
    {
        _config = configuration.GetSection("Jwt");
        _userRepository = userRepository;
        _fileService = fileService;
        _mapper = mapper;
        _cache = memoryCache;
        _dbSet = appDbContext;
        _emailService = emailService;
    }
    public async Task<string> LoginAsync(UserForLoginDto dto)
    {
        var user = await _userRepository.FindByEmailAsync(dto.Email);
        if (user is null)
            throw new HttpStatusCodeException(HttpStatusCode.NotFound, "Email not valid!");

        var isTrue = PasswordHasher.Verify(dto.Password, user.Password);
        return isTrue
            ? GeneratedToken(user) 
            : throw new HttpStatusCodeException(HttpStatusCode.NotFound, "Password not valid!");
    }

    public async Task<string> RegisterAsync(UserForCreationDto dto)
    {
        var checkUser = await _userRepository.FindByEmailAsync(dto.Email);
        if (checkUser is not null)
            throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "User already exist!");

        var imagePath = string.Empty;

        if (dto.Image is null)
            imagePath = $"{_fileService.ImageFolderName}/defaultAvatar.png";
        else
            imagePath = await _fileService.SaveImageAsync(dto.Image);

        var passwordHash = PasswordHasher.Hash(dto.Password).ToString();

        var user = _mapper.Map<User>(dto);

        user.Password = passwordHash;
        user.ImagePath = imagePath;

        await _userRepository.CreateAsync(user);
        await _dbSet.SaveChangesAsync();

        return GeneratedToken(user);
    }

    public async Task<bool> SendEmailAsync(string email)
    {
        var user = await _userRepository.FindByEmailAsync(email);
        if (user is null)
            throw new NotFoundException("User");

        var code = GeneratedCode();
        _cache.Set(email, code, TimeSpan.FromMinutes(2));

        await _emailService.SendAsync(email, code);

        return true;
    }
    public async Task<string> VerifyEmailAsync(string email, string code)
    {
        var user = await _userRepository.FindByEmailAsync(email);
        if (user is null)
            throw new NotFoundException("User");

        if(_cache.TryGetValue(email, out var exceptedCode))
        {
            if (exceptedCode.Equals(code))
                return GeneratedToken(user);

            throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "Code is not valid.");
        }

        throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "Code is expired!");
    }

    public async Task<bool> CheckConfirmPasswordAsync(string email, string password)
    {
        var user = await _userRepository.FindByEmailAsync(email);

        user.Password = PasswordHasher.Hash(password);
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateAsync(user);
        await _dbSet.SaveChangesAsync();

        return true;
    }


    private string GeneratedToken(User user)
    {
        var claims = new[]
{
            new Claim("Id", user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.UserRole.ToString()),
        };

        var secretKey = _config["SecretKey"];
        var issuer = _config["Issuer"];
        var audience = _config["Audience"];
        var expire = double.Parse(_config["Lifetime"]);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(issuer, audience, claims,
            expires: DateTime.Now.AddMinutes(expire),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    private string GeneratedCode()
        => new Random().Next(1000, 9999).ToString();
}
