using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly JobCandidateDbContext _context;

        public CandidateRepository(JobCandidateDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UpsertAsync(Candidate candidate, CancellationToken cancellationToken)
        {
            var existingUser = await _context.Candidates.FindAsync(candidate.Email);
            if (existingUser != null)
            {
                _context.Entry(existingUser).CurrentValues.SetValues(candidate);
            }
            else
            {
                await _context.Candidates.AddAsync(candidate);
            }

            return await _context.SaveChangesAsync() > 0;
        }
    }
}