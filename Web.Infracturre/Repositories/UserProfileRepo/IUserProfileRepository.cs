using Web.Domain.Entities;
using Web.Infracturre.Repositories.BaseRepo;
using Web.Model.EnumerationTypes;

namespace Web.Infracturre.Repositories.UserProfileRepo
{
    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        public Task<UserProfile> GetUserProfileByType(ProfileType type);
        public Task<UserProfile> GetUserProfileByName(string name);
    }
}
