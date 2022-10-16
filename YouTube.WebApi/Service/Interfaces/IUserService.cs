using System.Linq.Expressions;
using YouTube.WebApi.Domain.Commons;
using YouTube.WebApi.Domain.Entities;
using YouTube.WebApi.Service.DTOs.Users;

namespace YouTube.WebApi.Service.Interfaces;

public interface IUserService
{
    Task<UserForViewDto> UpdateAsync(long userId, UserForCreationDto dto);
    Task<UserForViewDto> GetAsync(Expression<Func<User, bool>> expression);

    Task DisableAsync(Expression<Func<User, bool>> expression);
    Task<IEnumerable<UserForViewDto>> GetAllAsync(Expression<Func<User, bool>>? expression = null,
        PaginationParameters? parameters = null);
}
