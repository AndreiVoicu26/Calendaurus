using LigaACLabs.Services;
using Microsoft.AspNetCore.Mvc;

namespace LigaACLabs.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectsController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        [HttpGet("{userId}")]
        public IActionResult GetSubjects(Guid userId)
        {
            var results = _subjectRepository.GetSubjectsForUser(userId);
            return Ok(results);
        }

        [HttpGet("{subjectId}/labs")]
        public IActionResult GetLabSessions(Guid subjectId)
        {
            var results = _subjectRepository.GetLabOptions(subjectId);
            return Ok(results);
        }
    }
}
