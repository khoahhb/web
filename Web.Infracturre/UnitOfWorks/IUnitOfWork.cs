namespace Web.Infracturre.UnitOfWorks
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
