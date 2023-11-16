using System.Linq.Expressions;

namespace Web.Infracturre.Interfaces
{
    public interface IReposiotry <T> where T : class
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, object>>[] includeProperties = null);
        Task<T> GetSingleById(object id, Expression<Func<T, object>>[] includeProperties = null);
        Task<T> GetSingleByCondition(Expression<Func<T, bool>> expression, Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> expression);
        Task Insert(T entity);
        Task Insert(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> expression);
        Task Commit();
        IQueryable<T> GetQuery(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includeProperties);
    }
}
