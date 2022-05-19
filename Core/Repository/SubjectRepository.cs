using Core.IRepository;
using StudentWebapp.Data;
using StudentWebapp.Models;

namespace Core.Repository
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}