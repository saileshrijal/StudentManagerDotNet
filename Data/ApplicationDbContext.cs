using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentWebapp.Models;

namespace StudentWebapp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Student>? Students { get; set; }
    public DbSet<Faculty>? Faculties { get; set; }
    public DbSet<Subject>? Subjects { get; set; }
    public DbSet<StudentSubject>? studentSubjects { get; set; }
}
