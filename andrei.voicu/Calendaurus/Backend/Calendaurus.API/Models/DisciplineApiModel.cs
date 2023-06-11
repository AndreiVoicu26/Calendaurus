namespace Calendaurus.API.Models
{
    public class DisciplineApiModel
    {
        public long Id { get; set; }

        public byte Year { get; set; }

        public string Faculty { get; set; } = null!;

        public string Name { get; set; } = null!;
    }
}