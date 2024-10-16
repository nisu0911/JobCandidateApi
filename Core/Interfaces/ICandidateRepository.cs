using Core.Entities;

namespace Core.Interfaces
{
    public interface ICandidateRepository
    {
        Task<bool> UpsertAsync(Candidate candidate, CancellationToken cancellationToken);
    }
}
