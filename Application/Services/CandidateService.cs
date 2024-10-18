using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ILogger<CandidateService> _logger;

        public CandidateService(ICandidateRepository candidateRepository, ILogger<CandidateService> logger)
        {
            _candidateRepository = candidateRepository;
            _logger = logger;
        }

        public async Task<bool> UpsertAsync(Candidate candidate, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Inside upsertAsync method");
            try
            {
                return await _candidateRepository.UpsertAsync(candidate, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upsert candidate.");
                throw new ApplicationException("Could not upsert candidate.", ex);
            }
        }
    }
}
