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

        public Repository(DbFactory dbFactory, IHttpContextAccessor httpContextAccessor)
        {
            _dbFactory = dbFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Create(T entity)
        {
            SetIdAndAuditInfo(entity);

            await DbSet.AddAsync(entity);
        }


        public async Task Create(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                SetIdAndAuditInfo(entity);
            }

            await DbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            SetIdAndAuditInfo(entity, true);
            DbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public void Delete(Expression<Func<T, bool>> expression)
        {
            var entities = DbSet.Where(expression).ToList();
            if (entities.Count > 0)
                DbSet.RemoveRange(entities);
        }

        public async Task<T> GetOne(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet;
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return await query.Where(expression).FirstOrDefaultAsync();
        }

        public async Task<T> GetOneById(object id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet;
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);
            return await query.SingleOrDefaultAsync(e => EF.Property<object>(e, "Id").Equals(id));
        }

        public async Task<IEnumerable<T>> GetMany(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet;
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return await query.Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet;
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);
            return await query.ToListAsync();
        }

        public string? GetCurrentUserName()
        {
            var response = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return response;
        }

        private void SetIdAndAuditInfo(T entity, bool isUpdate = false)
        {
            var baseEntity = (IBaseEntity<Guid>)entity;
            baseEntity.Id = baseEntity.Id != null && baseEntity.Id != Guid.Empty ? baseEntity.Id : Guid.NewGuid();

            if (entity is IAuditEntity auditEntity)
            {
                var currentUserId = GetCurrentUserId() ?? baseEntity?.Id ?? Guid.Empty;
                auditEntity.UpdatedAt = DateTime.UtcNow;
                auditEntity.UpdatedBy = currentUserId;

                if (!isUpdate)
                {
                    auditEntity.CreatedAt = auditEntity.UpdatedAt;
                    auditEntity.CreatedBy = currentUserId;
                }

                if (entity is Credential credential)
                {
                    credential.UpdatedBy = credential.UserId;

                    if (!isUpdate)
                    {
                        credential.CreatedBy = credential.UserId;
                    }
                }
            }
        }


        private Guid? GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value != null
                   ? Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
                   : null;
        }
    }
}
