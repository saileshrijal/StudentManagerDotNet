using Core.IRepository;
using StudentWebapp.Data;
using StudentWebapp.Models;

namespace Core.Repository
{
    public class FacultyRepository : GenericRepository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}