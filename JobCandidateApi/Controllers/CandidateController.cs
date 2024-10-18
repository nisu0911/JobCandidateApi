using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JobCandidateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        private readonly ILogger<CandidateController> _logger;

        public CandidateController(ICandidateService candidateService, ILogger<CandidateController> logger)
        {
            _candidateService = candidateService;
            _logger = logger;
        }

        [HttpPut]
        public async Task<IActionResult> UpsertAsync(Candidate candidate, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Inside upsert");
            var result = await _candidateService.UpsertAsync(candidate, cancellationToken);

            return result ? Ok() : StatusCode(500, "Error during upsert operation.");
        }
    }
}
