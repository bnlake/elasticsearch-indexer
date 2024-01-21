using System;
using System.Threading;
using System.Threading.Tasks;
using indexer.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace indexer.Application.Jobs;

public class IndexAllContentJob(SemaphoreSlim Semaphore, ILogger<IndexAllContentJob> Logger) : IJob
{
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        Logger.LogInformation("Index all content job started");

        await Task.Delay(TimeSpan.FromSeconds(2));

        Logger.LogInformation("Index all content job completed");
    }
}