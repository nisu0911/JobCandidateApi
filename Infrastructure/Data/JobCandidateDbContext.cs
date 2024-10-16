using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class JobCandidateDbContext : DbContext
    {
        public JobCandidateDbContext(DbContextOptions<JobCandidateDbContext> options)
            : base(options)
        { }

        public DbSet<Candidate> Candidates { get; set; }
    }
}