namespace LigaACLabs.Models.Entities
{
    public class Subject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Lab> Labs { get; set; }
    }
}
