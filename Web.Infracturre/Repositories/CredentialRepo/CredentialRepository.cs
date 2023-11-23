using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Web.Domain.Entities;
using Web.Infracturre.AuthenService;
using Web.Infracturre.DbFactories;
using Web.Infracturre.Repositories.BaseRepo;

namespace Web.Infracturre.Repositories.CredentialRepo
{
    public class CredentialRepository : Repository<Credential>, ICredentialRepository
    {
        private readonly IConfiguration _configuration;

        public CredentialRepository(DbFactory dbFactory, IAuthorizedUserService authorizedUserService, IConfiguration configuration) : base(dbFactory, authorizedUserService) 
        {
            _configuration = configuration;
        }

        public async Task<bool> IsValidAsync(string token)
        {
            var credential = await this.GetOneAsync(cre => cre.Token == token);
            return credential != null ? credential.CreatedAt.AddMinutes(Double.Parse(_configuration["Jwt:ExpireTime"])) > DateTime.UtcNow : false;
        }

        public bool IsValid(string token)
        {
            var credential = this.GetOne(cre => cre.Token == token);
            return credential != null ? credential.CreatedAt.AddMinutes(Double.Parse(_configuration["Jwt:ExpireTime"])) > DateTime.UtcNow : false;
        }
    }
}
