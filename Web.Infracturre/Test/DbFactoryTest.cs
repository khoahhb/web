using Microsoft.EntityFrameworkCore;
using Web.Domain.Test.Context;

namespace Web.Infracturre.Test
{
    public class DbFactoryTest : IDisposable
    {
        private bool _disposed;
        private Func<AppDbContext> _instanceFunc;
        private DbContext _dbContext;
        public DbContext DbContext => _dbContext ?? (_dbContext = _instanceFunc.Invoke());

        public DbFactoryTest(Func<AppDbContext> dbContextFactory)
        {
            _instanceFunc = dbContextFactory;
        }

        public void Dispose()
        {
            if (!_disposed && _dbContext != null)
            {
                _disposed = true;
                _dbContext.Dispose();
            }
        }
    }
}
