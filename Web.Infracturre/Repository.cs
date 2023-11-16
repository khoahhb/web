using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Web.Domain.Context;
using Web.Domain.Entities;
using Web.Infracturre.Interfaces;

namespace Web.Infracturre
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbFactory _dbFactory;
        private DbSet<T> _dbSet;

        protected DbSet<T> DbSet
        {
            get => _dbSet ?? (_dbSet = _dbFactory.DbContext.Set<T>());
        }

        public Repository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task Create(T entity)
        {
            await DbSet.AddAsync(entity);
        }

        public async Task Create(IEnumerable<T> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
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
    }
}
