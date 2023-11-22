using System.Linq.Expressions;
using Web.Domain.Entities;
using Web.Infracturre.Repositories.BaseRepo;

namespace Web.Infracturre.Repositories.UserRepo
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetUserByEmail(string email, params Expression<Func<User, object>>[] includeProperties);
        public Task<User> GetUserByUsername(string username, params Expression<Func<User, object>>[] includeProperties);
        public Task<User> GetUserByFullname(string fullname, params Expression<Func<User, object>>[] includeProperties);
    }
}
