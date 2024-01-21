using indexer.Application.Jobs;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using System.Threading.Tasks;

namespace indexer.Application.Tests;

[TestFixture]
public class IndexAllContentJobTests
{
    private readonly Mock<ILogger<IndexAllContentJob>> mockLogger = new();
    private SemaphoreSlim? semaphore;

    [TearDown]
    public void TearDown()
    {
        semaphore.Dispose();
    }

    [Test]
    public void JobInitializes_Succeeds()
    {
        semaphore = new SemaphoreSlim(1);

        IndexAllContentJob job = new(semaphore, mockLogger.Object);

        Assert.That(job, Is.Not.Null);
    }

    [Test]
    public async Task ReleasesLock_Succeeds()
    {
        var cancellationSource = new CancellationTokenSource();
        var semaphore = new SemaphoreSlim(1);
        IndexAllContentJob job = new(semaphore, mockLogger.Object);

        await job.ExecuteAsync(cancellationSource.Token);

        Assert.That(semaphore.CurrentCount, Is.EqualTo(1));
    }
}