﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;
using Web.Domain.Entities;
using Web.Infracturre.Interfaces;

namespace Web.Infracturre
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
            if (entity is IAuditEntity auditEntity)
            {
                var currentUser = GetCurrentUserName() ?? (entity as User)?.Fullname;

                auditEntity.CreatedAt = DateTime.UtcNow;
                auditEntity.UpdatedAt = DateTime.UtcNow;
                auditEntity.CreatedBy = currentUser;
                auditEntity.UpdatedBy = currentUser;
            }

            await DbSet.AddAsync(entity);
        }


        public async Task Create(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
                {
                    ((IAuditEntity)entity).UpdatedAt = DateTime.UtcNow;
                    ((IAuditEntity)entity).UpdatedBy = GetCurrentUserName();
                }
            }
                
            await DbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            {
                ((IAuditEntity)entity).UpdatedAt = DateTime.UtcNow;
                ((IAuditEntity)entity).UpdatedBy = GetCurrentUserName();
            }
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

        public async Task<T> GetOneId(object id, params Expression<Func<T, object>>[] includeProperties)
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
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}
