using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ViewModels
{
    public class SubjectViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Subject Name")]
        [Remote(action: "VerifySubjectName", controller: "Subject", AdditionalFields = "InitialSubjectName")]
        public string? SubjectName { get; set; }
        public string? Description { get; set; }

        public string? InitialSubjectName { get; set; }
    }
}