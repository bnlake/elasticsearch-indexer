using indexer.Application.Jobs;
using indexer.Domain.Models;
using indexer.Repository;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace indexer.Application.Tests;

[TestFixture]
public class IndexAllContentJobTests
{
    private readonly Mock<IContentRepository> mockRepository = new();
    private readonly Mock<ILogger<IndexAllContentJob>> mockLogger = new();

    [OneTimeSetUp]
    public void BeforeAll()
    {
        mockRepository.Setup(m => m.GetAllContentAsync().Result).Returns(new List<Content>() { new Content { Id = 1, Name = "First Document" } });
    }

    [TearDown]
    public void TearDown()
    {
        // semaphore.Dispose();
    }

    [Test]
    public async Task ExecuteAsync_CallsRepository_Succeeds()
    {
        var job = new IndexAllContentJob(mockRepository.Object, mockLogger.Object);

        await job.ExecuteAsync(new CancellationToken());

        mockRepository.Verify(r => r.GetAllContentAsync(), Times.Once);
    }
}