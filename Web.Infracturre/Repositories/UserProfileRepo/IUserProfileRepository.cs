using Web.Domain.Entities;
using Web.Infracturre.Repositories.BaseRepo;
using Web.Model.EnumerationTypes;

namespace Web.Infracturre.Repositories.UserProfileRepo
{
    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        public List<UserProfile> GetUserProfileByType(ProfileType type);
    }
}
