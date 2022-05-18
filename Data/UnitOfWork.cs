using Core.IConfiguration;
using Core.IRepository;
using Core.Repository;
using StudentWebapp.Data;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public IFacultyRepository Faculty { get; private set; }
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Faculty = new FacultyRepository(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async void DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}