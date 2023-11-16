using System.Linq.Expressions;

namespace Web.Infracturre.Test.Interfaces
{
    public interface IRepositoryTest<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        IQueryable<T> List(Expression<Func<T, bool>> expression);
    }
}
