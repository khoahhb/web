using System.Linq.Expressions;

namespace Web.Infracturre.Repositories.BaseRepo
{
    public interface IRepository<T> where T : class
    {
        T GetOneById(object Id, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetOneByIdAsync(object Id);
        T GetOne(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetOneAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetMany(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> GetQuery();
    }
}
