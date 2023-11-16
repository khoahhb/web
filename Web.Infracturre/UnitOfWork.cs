using Microsoft.EntityFrameworkCore;
using Web.Domain.Context;
using Web.Domain.Entities;
using Web.Infracturre.Interfaces;

namespace Web.Infracturre
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbFactory _dbFactory;

        public UnitOfWork(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public Task<int> CommitAsync()
        {
            return _dbFactory.DbContext.SaveChangesAsync();
        }
    }
}
