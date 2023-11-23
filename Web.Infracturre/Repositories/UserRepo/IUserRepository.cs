using System.Linq.Expressions;
using Web.Domain.Entities;
using Web.Infracturre.Repositories.BaseRepo;

namespace Web.Infracturre.Repositories.UserRepo
{
    public interface IUserRepository : IRepository<User>
    {
        public User GetByUsername(string username, params Expression<Func<User, object>>[] includeProperties);
        public User GetByEmail(string email, params Expression<Func<User, object>>[] includeProperties);
    }
}
