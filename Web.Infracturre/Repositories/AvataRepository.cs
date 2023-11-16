using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Web.Domain.Entities;
using Web.Infracturre.Interfaces;
using Web.Model.Enum;

namespace Web.Infracturre.Repositories
{
    public class AvataRepository : Repository<Avatar>, IAvatarRepository
    {
        public AvataRepository(DbFactory dbFactory) : base(dbFactory) { }

        public async Task<Avatar> GetAvatarByFileName(string filename)
        {
            return await this.GetOne(u => u.FileName == filename);
        }

        public async Task<IEnumerable<Avatar>> GetAvatarsByMimeType(string mime)
        {
            return await this.GetMany(u => u.MimeType == mime);
        }

        public async Task<IEnumerable<Avatar>> GetAvatarsByPublishStatus(bool status)
        {
            return await this.GetMany(u => u.IsPublished == status);
        }

        public Task CreateAvatar(Avatar entity)
        {
            return this.Create(entity);
        }

        public Task CreateMultiple(IEnumerable<Avatar> entities)
        {
            return this.Create(entities);
        }

        public void DeleteAvatar(Avatar entity)
        {
            this.Delete(entity);
        }

        public void DeleteMultipleAvatar(Expression<Func<Avatar, bool>> expression)
        {
            this.Delete(expression);
        }

        public Task<IEnumerable<Avatar>> GetAllAvatars(params Expression<Func<Avatar, object>>[] includeProperties)
        {
            return this.GetAll(includeProperties);
        }

        public Task<Avatar> GetAvatarByCondition(Expression<Func<Avatar, bool>> expression, params Expression<Func<Avatar, object>>[] includeProperties)
        {
            return this.GetOne(expression, includeProperties);
        }

        public Task<Avatar> GetAvatarById(object id, params Expression<Func<Avatar, object>>[] includeProperties)
        {
            return this.GetOneId(id, includeProperties);
        }

        public Task<IEnumerable<Avatar>> GetAvatarsByCondition(Expression<Func<Avatar, bool>> expression, params Expression<Func<Avatar, object>>[] includeProperties)
        {
            return this.GetMany(expression, includeProperties);
        }

        public void UpdateAvatar(Avatar entity)
        {
            this.Update(entity);
        }
    }
}
