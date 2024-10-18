using Application.Services;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.UnitTests
{
    [TestFixture]
    public class CandidateServiceTests
    {
        private Mock<ICandidateRepository> _candidateRepoMock;
        private Mock<ILogger<CandidateService>> _loggerMock;
        private CandidateService _candidateService;

        [SetUp]
        public void SetUp()
        {
            _candidateRepoMock = new Mock<ICandidateRepository>();
            _loggerMock = new Mock<ILogger<CandidateService>>();
            _candidateService = new CandidateService(_candidateRepoMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task UpsertCandidate_CallsRepositoryMethod()
        {
            var cancellationToken = new CancellationToken();

            // Arrange
            var candidate = new Candidate
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
            await _candidateService.UpsertAsync(candidate, cancellationToken);

            // Assert
            _candidateRepoMock.Verify(repo => repo.UpsertAsync(candidate, cancellationToken), Times.Once);
        }

        [Test]
        public void UpsertCandidate_ThrowsApplicationException_OnDbUpdateException()
        {
            var cancellationToken = new CancellationToken();

            // Arrange
            var candidate = new Candidate
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

            _candidateRepoMock
                .Setup(repo => repo.UpsertAsync(candidate, cancellationToken))
                .ThrowsAsync(new DbUpdateException());

            // Act & Assert
            var exception = Assert.ThrowsAsync<ApplicationException>(() => _candidateService.UpsertAsync(candidate, cancellationToken));
            Assert.That(exception.Message, Does.Contain("Could not upsert candidate."));
        }
    }
}
