namespace Web.Infracturre.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
