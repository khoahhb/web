namespace Web.Infracturre.Test.Interfaces
{
    public interface IUnitOfWorkTest
    {
        Task<int> CommitAsync();
    }
}
