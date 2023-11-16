using System.Linq.Expressions;
using Web.Domain.Entities;
using Web.Model.Enum;

namespace Web.Infracturre.Interfaces
{
    public interface IUserProfileRepository
    {
        public Task<UserProfile> GetUserProfileByType(ProfileType type);
        public Task<UserProfile> GetUserProfileByName(string name);

        public Task CreateUserProfile(UserProfile entity);
        public Task CreateMultiple(IEnumerable<UserProfile> entities);
        public void UpdateUserProfile(UserProfile entity);
        public void DeleteUserProfile(UserProfile entity);
        public void DeleteMultipleUserProfile(Expression<Func<UserProfile, bool>> expression);
        public Task<UserProfile> GetUserProfileByCondition(Expression<Func<UserProfile, bool>> expression, params Expression<Func<UserProfile, object>>[] includeProperties);
        public Task<UserProfile> GetUserProfileById(object id, params Expression<Func<UserProfile, object>>[] includeProperties);
        public Task<IEnumerable<UserProfile>> GetUserProfilesByCondition(Expression<Func<UserProfile, bool>> expression, params Expression<Func<UserProfile, object>>[] includeProperties);
        public Task<IEnumerable<UserProfile>> GetAllUserProfiles(params Expression<Func<UserProfile, object>>[] includeProperties);
    }
}
