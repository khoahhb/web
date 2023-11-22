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

        public User GetByEmail(string email, params Expression<Func<User, object>>[] includeProperties)
        {
            return this.GetOne(u => u.Email == email, includeProperties);
        }

        public User GetByUsername(string username, params Expression<Func<User, object>>[] includeProperties)
        {
            return this.GetOne(u => u.Username == username, includeProperties);
        }
    }
}
