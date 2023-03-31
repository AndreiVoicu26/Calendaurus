namespace LigaACLabs.Models.Entities
{
    public class Preference
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid LabId { get; set; }
    }
}
