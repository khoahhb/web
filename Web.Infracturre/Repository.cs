using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Web.Domain.Context;
using Web.Domain.Entities;
using Web.Infracturre.Interfaces;

namespace Web.Infracturre
{
    public class Repository<T> : IReposiotry<T> where T : class
    {
        private ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, object>>[] includeProperties = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                    query = query.Include(includeProperty);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetSingleById(object id, Expression<Func<T, object>>[] includeProperties = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                    query = query.Include(includeProperty);
            }
            return await query.SingleOrDefaultAsync(e => EF.Property<object>(e, "Id").Equals(id));
        }

        public async Task<T> GetSingleByCondition(Expression<Func<T, bool>> expression, Expression<Func<T, object>>[] includeProperties = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                    query = query.Include(includeProperty);
            }
           
            return await query.Where(expression).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task Insert(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task Insert(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Delete(Expression<Func<T, bool>> expression)
        {
            var entities = _context.Set<T>().Where(expression).ToList();
            if (entities.Count > 0)
                _context.Set<T>().RemoveRange(entities);
        }
        public virtual IQueryable<T> Table => _context.Set<T>();

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = _context.Set<T>();
            return query.Where(expression);
        }
        public IQueryable<T> GetQuery(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = query.Where(expression);
            foreach (var item in includeProperties)
            {
                query.Include(item);
            }
            return query.Where(expression);
        }
        public async Task Commit()
        {
           await _context.SaveChangesAsync();
        }
    }
}
