using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YouTube.WebApi.Data.DbContexts;
using YouTube.WebApi.Data.Interfaces;
using YouTube.WebApi.Domain.Entities;

#pragma warning disable

namespace YouTube.WebApi.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbSet;

        public UserRepository(AppDbContext appDbContext)
        {
            _dbSet = appDbContext;
        }
        public async Task<User> CreateAsync(User user)
        {
            var entity = await _dbSet.Users.AddAsync(user);

            return entity.Entity;
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            var entity = await _dbSet.Users.FirstOrDefaultAsync(p => p.Email.Equals(email));

            return entity;
        }

        public IQueryable<User> GetAll(Expression<Func<User, bool>>? expression = null, bool isTracking = true)
        {
            var entities = expression is null ? _dbSet.Users : _dbSet.Users.Where(expression);

            return isTracking ? entities : entities.AsNoTracking();
        }

        public async Task<User?> GetAsync(Expression<Func<User, bool>> expression)
            => await _dbSet.Users.FirstOrDefaultAsync(expression);

        public async Task<User> UpdateAsync(User user)
        {
            var entity = _dbSet.Update(user);

            return entity.Entity;
        }
    }
}
