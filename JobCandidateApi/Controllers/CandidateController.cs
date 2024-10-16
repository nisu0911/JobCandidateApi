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

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost("upsert")]
        public async Task<IActionResult> UpsertAsync(Candidate candidate, CancellationToken cancellationToken)
        {
            var result = await _candidateService.UpsertAsync(candidate, cancellationToken);

            return result ? Ok() : StatusCode(500, "Error during upsert operation.");
        }
    }
}
