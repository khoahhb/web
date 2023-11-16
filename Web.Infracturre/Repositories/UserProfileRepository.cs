using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using Web.Domain.Entities;
using Web.Infracturre.Interfaces;
using Web.Model.Enum;

namespace Web.Infracturre.Repositories
{
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(DbFactory dbFactory, IHttpContextAccessor httpContextAccessor) : base(dbFactory, httpContextAccessor) { }

        public Task CreateMultiple(IEnumerable<UserProfile> entities)
        {
            return this.Create(entities);
        }

        public Task CreateUserProfile(UserProfile entity)
        {
            return this.Create(entity);
        }

        public void DeleteMultipleUserProfile(Expression<Func<UserProfile, bool>> expression)
        {
            this.Delete(expression);
        }

        public void DeleteUserProfile(UserProfile entity)
        {
            this.Delete(entity);
        }

        public Task<IEnumerable<UserProfile>> GetAllUserProfiles(params Expression<Func<UserProfile, object>>[] includeProperties)
        {
            return this.GetAll(includeProperties);
        }

        public Task<UserProfile> GetUserProfileByCondition(Expression<Func<UserProfile, bool>> expression, params Expression<Func<UserProfile, object>>[] includeProperties)
        {
            return this.GetOne(expression, includeProperties);
        }

        public Task<UserProfile> GetUserProfileById(object id, params Expression<Func<UserProfile, object>>[] includeProperties)
        {
            return this.GetOneId(id, includeProperties);
        }

        public async Task<UserProfile> GetUserProfileByName(string name)
        {
            return await this.GetOne(u => u.Name == name);
        }

        public async Task<UserProfile> GetUserProfileByType(ProfileType type)
        {
            return await this.GetOne(u => u.Type == type);
        }

        public Task<IEnumerable<UserProfile>> GetUserProfilesByCondition(Expression<Func<UserProfile, bool>> expression, params Expression<Func<UserProfile, object>>[] includeProperties)
        {
            return this.GetMany(expression, includeProperties);
        }

        public void UpdateUserProfile(UserProfile entity)
        {
            this.Update(entity);
        }
    }
}
