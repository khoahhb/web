using Microsoft.AspNetCore.Http;
using Web.Domain.Entities;
using Web.Infracturre.DbFactories;
using Web.Infracturre.Repositories.BaseRepo;

namespace Web.Infracturre.Repositories.AvatarRepo
{
    public class AvataRepository : Repository<Avatar>, IAvatarRepository
    {
        public AvataRepository(DbFactory dbFactory, IHttpContextAccessor httpContextAccessor) : base(dbFactory, httpContextAccessor) { }

        List<Avatar> IAvatarRepository.GetAvatarsByPublishStatus(bool status)
        {
            return this.GetMany(a => a.IsPublished == status).ToList();
        }
    }
}
