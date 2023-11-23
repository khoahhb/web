using System.Linq.Expressions;
using Web.Domain.Entities;
using Web.Infracturre.Repositories.BaseRepo;

namespace Web.Infracturre.Repositories.CredentialRepo
{
    public interface ICredentialRepository : IRepository<Credential>
    {
        public Task<bool> IsValidAsync(string token);
        public bool IsValid(string token);
    }
}
