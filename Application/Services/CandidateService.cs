using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<bool> UpsertAsync(Candidate candidate, CancellationToken cancellationToken)
        {
            return await _candidateRepository.UpsertAsync(candidate, cancellationToken);
        }
    }
}
