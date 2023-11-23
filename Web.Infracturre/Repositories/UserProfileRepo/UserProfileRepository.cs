using Microsoft.AspNetCore.Http;
using Web.Domain.Entities;
using Web.Infracturre.AuthenService;
using Web.Infracturre.DbFactories;
using Web.Infracturre.Repositories.BaseRepo;
using Web.Model.EnumerationTypes;

namespace Web.Infracturre.Repositories.UserProfileRepo
{
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(DbFactory dbFactory, IAuthorizedUserService authorizedUserService) : base(dbFactory, authorizedUserService) { }

        public List<UserProfile> GetUserProfileByType(ProfileType type)
        {
            return this.GetMany(prty => prty.Type == type).ToList();
        }
    }
}
