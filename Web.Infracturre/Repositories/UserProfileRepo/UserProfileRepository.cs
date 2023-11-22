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

        public async Task<UserProfile> GetUserProfileByName(string name)
        {
            return await GetOne(u => u.Name == name);
        }

        public async Task<UserProfile> GetUserProfileByType(ProfileType type)
        {
            return await GetOne(u => u.Type == type);
        }
    }
}
