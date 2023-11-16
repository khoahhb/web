using System.Linq.Expressions;
using Web.Domain.Entities;

namespace Web.Infracturre.Interfaces
{
    public interface IAvatarRepository
    {
        public Task<Avatar> GetAvatarByFileName(string filename);
        public Task<IEnumerable<Avatar>> GetAvatarsByPublishStatus(bool status);
        public Task<IEnumerable<Avatar>> GetAvatarsByMimeType(string mime);
        public Task CreateAvatar(Avatar entity);
        public Task CreateMultiple(IEnumerable<Avatar> entities);
        public void UpdateAvatar(Avatar entity);
        public void DeleteAvatar(Avatar entity);
        public void DeleteMultipleAvatar(Expression<Func<Avatar, bool>> expression);
        public Task<Avatar> GetAvatarByCondition(Expression<Func<Avatar, bool>> expression, params Expression<Func<Avatar, object>>[] includeProperties);
        public Task<Avatar> GetAvatarById(object id, params Expression<Func<Avatar, object>>[] includeProperties);
        public Task<IEnumerable<Avatar>> GetAvatarsByCondition(Expression<Func<Avatar, bool>> expression, params Expression<Func<Avatar, object>>[] includeProperties);
        public Task<IEnumerable<Avatar>> GetAllAvatars(params Expression<Func<Avatar, object>>[] includeProperties);
    }
}
