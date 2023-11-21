using System.Linq.Expressions;

namespace Web.Infracturre.Repositories.BaseRepo
{
    public interface IRepository<T> where T : class
    {
        Task Create(T entity);
        Task Create(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> expression);
        Task<T> GetOne(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetOneById(object id, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetMany(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includeProperties);
    }
}
