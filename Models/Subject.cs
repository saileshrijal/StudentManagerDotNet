using System.ComponentModel.DataAnnotations;

namespace StudentWebapp.Models
{
    public class Subject
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Subject Name")]
        public string? SubjectName { get; set; }
        public string? Description { get; set; }
    }
}