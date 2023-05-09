namespace Calendaurus.API.Requests
{
    public class CreatePracticalLessonRequest
    {
        public string Type { get; set; } = null!;
        public string? Description { get; set; }
    }
}
