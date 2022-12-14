using System.Linq.Expressions;
using YouTube.WebApi.Domain.Entities;

namespace YouTube.WebApi.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<User?> FindByEmailAsync(string email);
        //Task ChangeActivityAsync(bool active, Expression<Func<User, bool>> expression);
        Task<User?> GetAsync(Expression<Func<User, bool>> expression);
        IQueryable<User> GetAll(Expression<Func<User, bool>>? expression = null, bool isTracking = true);
    }
}
