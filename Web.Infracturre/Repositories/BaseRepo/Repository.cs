using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;
using Web.Domain.Entities;
using Web.Infracturre.DbFactories;

namespace Web.Infracturre.Repositories.BaseRepo
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbFactory _dbFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private DbSet<T> _dbSet;

        protected DbSet<T> DbSet
        {
            get => _dbSet ?? (_dbSet = _dbFactory.DbContext.Set<T>());
        }
        public IQueryable<T> GetQuery()
        {
            return DbSet.AsNoTracking();
        }
        public Repository(DbFactory dbFactory, IHttpContextAccessor httpContextAccessor)
        {
            _dbFactory = dbFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public T GetOneById(object Id, params Expression<Func<T, object>>[] includeProperties)
        {
            var result = DbSet;

            foreach (var property in includeProperties)
                result.Include(property).Load();

            return result.Find(Id);
        }

        public async Task<T> GetOneByIdAsync(object Id)
        {
            return await DbSet.FindAsync(Id);
        }

        public T GetOne(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var result = DbSet;

            foreach (var property in includeProperties)
                result.Include(property).Load();

            return result.FirstOrDefault(expression);
        }

        public async Task<T> GetOneAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties)
        {
            var result = DbSet;

            foreach (var property in includeProperties)
                result.Include(property).Load();

            return await result.FirstOrDefaultAsync(expression);
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            var result = DbSet;

            foreach (var property in includeProperties)
                result.Include(property).Load();

            return result;
        }

        public IQueryable<T> GetMany(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties)
        {
            var result = DbSet.Where(expression);

            foreach (var property in includeProperties)
                result.Include(property).Load();

            return result;
        }

        public void Add(T entity)
        {
            if (typeof(IBaseEntity<Guid>).IsAssignableFrom(typeof(T)))
            {
                ((IBaseEntity<Guid>)entity).Id = Guid.NewGuid();
            }
            var baseEntity = (IBaseEntity<Guid>)entity;
            var currentUserId = GetCurrentUserId() ?? baseEntity.Id;
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            {
                ((IAuditEntity)entity).CreatedAt = DateTime.UtcNow;
                ((IAuditEntity)entity).UpdatedAt = DateTime.UtcNow;
                ((IAuditEntity)entity).CreatedBy = currentUserId;
                ((IAuditEntity)entity).UpdatedBy = currentUserId;
                if (entity is Credential credential)
                {
                    credential.CreatedBy = credential.UserId;
                    credential.UpdatedBy = credential.UserId;
                }
            }
            DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            var currentUserId = GetCurrentUserId();
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            {
                ((IAuditEntity)entity).UpdatedAt = DateTime.UtcNow;
                ((IAuditEntity)entity).UpdatedBy = (Guid )currentUserId;
            }
            DbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        private Guid? GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value != null
                   ? Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
                   : null;
        }
    }
}
