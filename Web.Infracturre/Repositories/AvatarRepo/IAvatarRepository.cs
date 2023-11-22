using Web.Domain.Entities;
using Web.Infracturre.Repositories.BaseRepo;

namespace Web.Infracturre.Repositories.AvatarRepo
{
    public interface IAvatarRepository : IRepository<Avatar>
    {
        public Task<Avatar> GetAvatarByFileName(string filename);
        public Task<IEnumerable<Avatar>> GetAvatarsByPublishStatus(bool status);
        public Task<IEnumerable<Avatar>> GetAvatarsByMimeType(string mime);
    }
}
