
using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Infrastructure.UnitTests
{
    [TestFixture]
    public class CandidateRepositoryTests
    {
        private Mock<ILogger<CandidateRepository>> _loggerMock;
        private JobCandidateDbContext _context;
        private CandidateRepository _candidateRepository;

        [SetUp]
        public void SetUp()
        {
            // Set up an in-memory database for testing
            var options = new DbContextOptionsBuilder<JobCandidateDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new JobCandidateDbContext(options);
            _loggerMock = new Mock<ILogger<CandidateRepository>>();
            _candidateRepository = new CandidateRepository(_context, _loggerMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task UpsertCandidate_InsertsNewCandidate()
        {
            var cancellationToken = new CancellationToken();
            
            // Arrange
            var newCandidate = new Candidate
            {
                Email = "test@test.com",
                FirstName = "test",
                LastName = "test",
                PhoneNumber = "1234567890",
                TimeInterval = "test",
                Github = "test",
                Linkedin = "test",
                Comment = "test"
            };

            // Act
            await _candidateRepository.UpsertAsync(newCandidate, cancellationToken);

            // Assert
            var candidate = await _context.Candidates.FindAsync(newCandidate.Email);
            Assert.IsNotNull(candidate);
            Assert.That(candidate.Email, Is.EqualTo(newCandidate.Email));
        }

        [Test]
        public async Task UpsertCandidate_UpdatesExistingCandidate()
        {
            var cancellationToken = new CancellationToken();
            
            // Arrange
            var existingCandidate = new Candidate
            {
                Email = "test@test.com",
                FirstName = "test",
                LastName = "test",
                PhoneNumber = "1234567890",
                TimeInterval = "test",
                Github = "test",
                Linkedin = "test",
                Comment = "test"
            };

            _context.Candidates.Add(existingCandidate);
            await _context.SaveChangesAsync();

            var updatedCandidate = new Candidate
            {
                Email = "test@test.com",
                FirstName = "test1",
                LastName = "test1",
                PhoneNumber = "1234567890",
                TimeInterval = "test",
                Github = "test",
                Linkedin = "test",
                Comment = "test"
            };

            // Act
            await _candidateRepository.UpsertAsync(updatedCandidate, cancellationToken);

            // Assert
            var candidate = await _context.Candidates.FindAsync(updatedCandidate.Email);
            Assert.That(candidate, Is.Not.Null);
            Assert.That(candidate.Email, Is.EqualTo(updatedCandidate.Email));
        }

        [Test]
        public void UpsertCandidate_ThrowsDbUpdateException()
        {
            var cancellationToken = new CancellationToken();
            
            // Arrange
            var candidateRepo = new CandidateRepository(_context, _loggerMock.Object);
            var invalidCandidate = new Candidate { Email = "test@test.com" };

            // Act & Assert
            Assert.ThrowsAsync<DbUpdateException>(async() => await candidateRepo.UpsertAsync(invalidCandidate, cancellationToken));
        }
    }
}
