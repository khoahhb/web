using Microsoft.AspNetCore.Http;
using Web.Domain.Entities;
using Web.Infracturre.DbFactories;
using Web.Infracturre.Repositories.BaseRepo;
using Web.Model.EnumerationTypes;

namespace Web.Infracturre.Repositories.UserProfileRepo
{
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(DbFactory dbFactory, IHttpContextAccessor httpContextAccessor) : base(dbFactory, httpContextAccessor) { }

        public List<UserProfile> GetUserProfileByType(ProfileType type)
        {
            return this.GetMany(prty => prty.Type == type).ToList();
        }
    }
}
