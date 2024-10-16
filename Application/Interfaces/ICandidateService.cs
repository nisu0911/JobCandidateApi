using Core.Entities;

namespace Application.Interfaces
{
    public interface ICandidateService
    {
        Task<bool> UpsertAsync(Candidate candidate, CancellationToken cancellationToken);
    }
}
