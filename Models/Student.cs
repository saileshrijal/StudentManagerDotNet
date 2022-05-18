using System.ComponentModel.DataAnnotations;

namespace StudentWebapp.Models
{
    public class Student
    {
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required, Display(Name = "Email Address"), DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        public string? Address { get; set; }

        public int FacultyId { get; set; }
        public Faculty? Faculty { get; set; }

        public List<Student>? Students { get; set; }
    }
}