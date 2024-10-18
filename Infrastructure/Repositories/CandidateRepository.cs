using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly JobCandidateDbContext _context;
        private readonly ILogger<CandidateRepository> _logger;

        public CandidateRepository(JobCandidateDbContext context, ILogger<CandidateRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> UpsertAsync(Candidate candidate, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Inside upsertAsync method");

            try
            {
                var existingUser = await _context.Candidates.FindAsync(candidate.Email, cancellationToken);
                if (existingUser != null)
                {
                    _context.Entry(existingUser).CurrentValues.SetValues(candidate);
                    _logger.LogInformation("Updating candidate");
                }
                else
                {
                    await _context.Candidates.AddAsync(candidate, cancellationToken);
                    _logger.LogInformation("Creating candidate");
                }
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error during upsert operation.");
                throw;
            }

        }
    }
}