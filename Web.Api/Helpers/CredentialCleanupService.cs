using Microsoft.Extensions.Configuration;
using Web.Infracturre.Repositories.CredentialRepo;
using Web.Infracturre.UnitOfWorks;

namespace Web.Api.Helpers
{
    public class CredentialCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _checkInterval;
        private readonly IConfiguration _configuration;


        public CredentialCleanupService(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _checkInterval = TimeSpan.FromMinutes(1);
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await DoWork(stoppingToken);
                await Task.Delay(_checkInterval, stoppingToken);
            }
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var credentialRepository = scope.ServiceProvider.GetRequiredService<ICredentialRepository>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var expiredCredentials = await credentialRepository.GetMany(cre => cre.CreatedAt.AddMinutes(Double.Parse(_configuration["CleanupIntervalMinute"])) < DateTime.UtcNow);

                foreach (var credential in expiredCredentials)
                {
                    credentialRepository.Delete(credential);
                }

                await unitOfWork.CommitAsync();
            }
        }
    }

}
