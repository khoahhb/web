using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using Web.Domain.Entities;
using Web.Infracturre.Interfaces;

namespace Web.Infracturre.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbFactory dbFactory, IHttpContextAccessor httpContextAccessor) : base(dbFactory, httpContextAccessor) { }

        public Task CreateMultiple(IEnumerable<User> entities)
        {
            return this.Create(entities);
        }

        public Task CreateUser(User entity)
        {
            return this.Create(entity);
        }

        public void DeleteMultipleUser(Expression<Func<User, bool>> expression)
        {
            this.Delete(expression);
        }

        public void DeleteUser(User entity)
        {
            this.Delete(entity);
        }

        public Task<IEnumerable<User>> GetAllUsers(params Expression<Func<User, object>>[] includeProperties)
        {
            return this.GetAll(includeProperties);
        }

        public async Task<User> GetUserByEmail(string email, params Expression<Func<User, object>>[] includeProperties)
        {
            return await this.GetOne(u => u.Email == email, includeProperties);
        }

        public async Task<User> GetUserByFullname(string fullname, params Expression<Func<User, object>>[] includeProperties)
        {
            return await this.GetOne(u => u.Fullname == fullname, includeProperties);
        }

        public Task<User> GetUserById(object id, params Expression<Func<User, object>>[] includeProperties)
        {
           return this.GetOneId(id, includeProperties);
        }

        public async Task<User> GetUserByUsername(string username, params Expression<Func<User, object>>[] includeProperties)
        {
            return await this.GetOne(u => u.Username == username, includeProperties);
        }

        public Task<IEnumerable<User>> GetUsersByCondition(Expression<Func<User, bool>> expression, params Expression<Func<User, object>>[] includeProperties)
        {
            return this.GetMany(expression, includeProperties);
        }

        public Task<User> GetUserByCondition(Expression<Func<User, bool>> expression, params Expression<Func<User, object>>[] includeProperties)
        {
            return this.GetOne(expression, includeProperties);
        }

        public void UpdateUser(User entity)
        {
            this.Update(entity);
        }
    }
}
