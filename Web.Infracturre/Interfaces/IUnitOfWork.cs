using Web.Domain.Entities;

namespace Web.Infracturre.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
