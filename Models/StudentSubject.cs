namespace StudentWebapp.Models
{
    public class StudentSubject
    {
        public Guid Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }

        public Student? Student { get; set; }
        public Subject? Subject { get; set; }
    }
}