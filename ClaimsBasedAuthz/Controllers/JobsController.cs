using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimsBasedAuthz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly ILogger<JobsController> _logger;

        public JobsController(ILogger<JobsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{jobId}")]
        [Authorize(Policy = "TenantAOnly")]
        public IActionResult GetJobById([FromRoute] int jobId)
        {
            return Ok(jobId);
        }

        [HttpGet]
        [Authorize(Policy = "TenantBOnly")]
        public IActionResult GetAllJobs()
        {
            return Ok("All Jobs");
        }
    }
}