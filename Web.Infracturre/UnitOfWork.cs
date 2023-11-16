using Microsoft.EntityFrameworkCore;
using Web.Domain.Context;
using Web.Domain.Entities;
using Web.Infracturre.Interfaces;

namespace Web.Infracturre
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private readonly ApplicationDbContext _context;

        private Repository<User> _repositoryUser;
        private Repository<UserProfile> _repositoryProfile;
        private Repository<Avatar> _repositoryAvatar;
        private bool _disposed;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public Repository<User> RepositoryUser { get { return _repositoryUser ??= new Repository<User>(_context); } }
        public Repository<UserProfile> RepositoryProfile { get { return _repositoryProfile ??= new Repository<UserProfile>(_context); } }
        public Repository<Avatar> RepositoryAvatar { get { return _repositoryAvatar ??= new Repository<Avatar>(_context); } }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if(disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
