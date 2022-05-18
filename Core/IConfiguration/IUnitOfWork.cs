using Core.IRepository;

namespace Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IFacultyRepository Faculty { get; }
        Task SaveChangesAsync();
    }
}