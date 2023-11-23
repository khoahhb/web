using Web.Domain.Entities;
using Web.Infracturre.Repositories.BaseRepo;

namespace Web.Infracturre.Repositories.AvatarRepo
{
    public interface IAvatarRepository : IRepository<Avatar>
    {
        public List<Avatar> GetAvatarsByPublishStatus(bool status);
    }
}
