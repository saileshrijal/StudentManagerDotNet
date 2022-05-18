using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace ViewModels
{
    public class FacultyViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Faculty Name")]
        [Remote(action: "VerifyFacultyName", controller: "Faculty")]
        public string? FacultyName { get; set; }
        public string? Description { get; set; }

    }
}