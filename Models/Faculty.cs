using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace StudentWebapp.Models
{
    public class Faculty
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Faculty Name")]
        [Remote(action: "VerifyFacultyName", controller: "Faculty")]
        public string? FacultyName { get; set; }
        public string? Description { get; set; }
    }
}