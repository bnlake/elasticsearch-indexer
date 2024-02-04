using indexer.Application.Interfaces;
using indexer.Application.Jobs;
using indexer.Application.Models;
using indexer.Domain.Models;
using indexer.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace indexer.Application.Tests;

[TestFixture]
public class IndexAllContentJobTests
{
    private readonly Mock<IContentRepository> mockRepository = new();
    private readonly Mock<IElasticSearchService> mockESService = new();
    private readonly Mock<IIndexDocumentProcesser> mockIndexDocumentProcesser = new();
    private readonly Mock<IOptionsSnapshot<IndexOptions>> mockIndexOptions = new();
    private readonly Mock<ILogger<IndexAllContentJob>> mockLogger = new();

    [OneTimeSetUp]
    public void BeforeAll()
    {
        var mockData = new Content[] { new() { Id = 1, Name = "Test Document" } };
        mockRepository.Setup(m => m.GetAllContentAsync()).Returns(GetContents());
        mockIndexOptions.Setup(o => o.Value).Returns(new IndexOptions { Name = "unit_test_index", Alias = "unit_test_alias" });
    }

    /// <summary>
    /// Helper method to return mock data for testing
    /// </summary>
    /// <returns></returns>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    private static async IAsyncEnumerable<Content> GetContents()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        yield return new Content { Id = 1, Name = "First Document" };
        yield return new Content { Id = 2, Name = "Second Document" };
    }

    /// <summary>
    /// Helper method to create our class to be tested
    /// </summary>
    /// <returns></returns>
    private IndexAllContentJob CreateJob()
    {
        return new IndexAllContentJob(mockRepository.Object,
            mockESService.Object,
            mockIndexDocumentProcesser.Object,
            mockIndexOptions.Object,
            mockLogger.Object);
    }

    [Test]
    public async Task ExecuteAsync_CallsRepository_Succeeds()
    {
        var job = CreateJob();

        await job.ExecuteAsync(new CancellationToken());

        mockRepository.Verify(r => r.GetAllContentAsync(), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_IndexDoesntExist_InvokesCreateIndex()
    {
        mockESService.Setup(e => e.CheckIndexExists(It.IsAny<string>())).Returns(false);
        var job = CreateJob();

        await job.ExecuteAsync(new CancellationToken());

        mockESService.Verify(e => e.CreateIndexAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_IndexExists_DoesntInvokeCreateIndex()
    {
        mockESService.Setup(e => e.CheckIndexExists(It.IsAny<string>())).Returns(true);
        var job = CreateJob();

        await job.ExecuteAsync(new CancellationToken());

        mockESService.Verify(e => e.CreateIndexAsync(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task ExecuteAsync_InvokesIndexProcesser_CorrectAmount()
    {
        mockESService.Setup(e => e.CheckIndexExists(It.IsAny<string>())).Returns(true);
        var job = CreateJob();

        await job.ExecuteAsync(new CancellationToken());

        mockIndexDocumentProcesser.Verify(p => p.AddContent(It.IsAny<Content>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
    }
}
