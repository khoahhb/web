using System.Linq.Expressions;
using Web.Domain.Entities;

namespace Web.Infracturre.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetUserByEmail(string email, params Expression<Func<User, object>>[] includeProperties);
        public Task<User> GetUserByUsername(string username, params Expression<Func<User, object>>[] includeProperties);
        public Task<User> GetUserByFullname(string fullname, params Expression<Func<User, object>>[] includeProperties);
        public Task CreateUser(User entity);
        public Task CreateMultiple(IEnumerable<User> entities);
        public void UpdateUser(User entity);
        public void DeleteUser(User entity);
        public void DeleteMultipleUser(Expression<Func<User, bool>> expression);
        public Task<User> GetUserByCondition(Expression<Func<User, bool>> expression, params Expression<Func<User, object>>[] includeProperties);
        public Task<User> GetUserById(object id, params Expression<Func<User, object>>[] includeProperties);
        public Task<IEnumerable<User>> GetUsersByCondition(Expression<Func<User, bool>> expression, params Expression<Func<User, object>>[] includeProperties);
        public Task<IEnumerable<User>> GetAllUsers(params Expression<Func<User, object>>[] includeProperties);
    }
}
