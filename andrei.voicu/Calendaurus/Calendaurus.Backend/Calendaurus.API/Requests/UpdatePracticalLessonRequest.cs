namespace Calendaurus.API.Requests
{
    public class UpdatePracticalLessonRequest
    {
        public string Type { get; set; } = null!;
        public string? Description { get; set; }
    }
}
