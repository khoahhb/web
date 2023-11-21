using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using Web.Domain.Entities;
using Web.Infracturre.DbFactories;
using Web.Infracturre.Repositories.BaseRepo;

namespace Web.Infracturre.Repositories.UserRepo
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbFactory dbFactory, IHttpContextAccessor httpContextAccessor) : base(dbFactory, httpContextAccessor) { }

        public async Task<User> GetUserByEmail(string email, params Expression<Func<User, object>>[] includeProperties)
        {
            return await GetOne(u => u.Email == email, includeProperties);
        }

        public async Task<User> GetUserByFullname(string fullname, params Expression<Func<User, object>>[] includeProperties)
        {
            return await GetOne(u => u.Fullname == fullname, includeProperties);
        }

        public async Task<User> GetUserByUsername(string username, params Expression<Func<User, object>>[] includeProperties)
        {
            return await GetOne(u => u.Username == username, includeProperties);
        }

    }
}
