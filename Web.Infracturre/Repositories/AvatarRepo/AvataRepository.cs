using Microsoft.AspNetCore.Http;
using Web.Domain.Entities;
using Web.Infracturre.DbFactories;
using Web.Infracturre.Repositories.BaseRepo;

namespace Web.Infracturre.Repositories.AvatarRepo
{
    public class AvataRepository : Repository<Avatar>, IAvatarRepository
    {
        public AvataRepository(DbFactory dbFactory, IHttpContextAccessor httpContextAccessor) : base(dbFactory, httpContextAccessor) { }

        public async Task<Avatar> GetAvatarByFileName(string filename)
        {
            return await GetOne(u => u.FileName == filename);
        }

        public async Task<IEnumerable<Avatar>> GetAvatarsByMimeType(string mime)
        {
            return await GetMany(u => u.MimeType == mime);
        }

        public async Task<IEnumerable<Avatar>> GetAvatarsByPublishStatus(bool status)
        {
            return await GetMany(u => u.IsPublished == status);
        }
    }
}
