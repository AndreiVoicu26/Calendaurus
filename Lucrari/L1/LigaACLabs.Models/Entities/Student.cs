namespace LigaACLabs.Models.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<Subject> Subjects { get; set; }
    }
}
