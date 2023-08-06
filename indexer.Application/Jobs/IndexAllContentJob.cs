using System.Threading;
using System.Threading.Tasks;
using indexer.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace indexer.Application.Jobs;

public class IndexAllContentJob : IJob
{
    private readonly ILogger<IndexAllContentJob> logger;
    private readonly SemaphoreSlim semaphore;

    public IndexAllContentJob(SemaphoreSlim semaphore, ILogger<IndexAllContentJob> logger)
    {
        this.logger = logger;
        this.semaphore = semaphore;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await semaphore.WaitAsync(cancellationToken);

        semaphore.Release();
    }
}