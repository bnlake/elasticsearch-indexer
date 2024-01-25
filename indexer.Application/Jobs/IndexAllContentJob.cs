using indexer.Application.Interfaces;
using indexer.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace indexer.Application.Jobs;

public class IndexAllContentJob(IContentRepository Repository, ILogger<IndexAllContentJob> Logger) : IJob<IndexAllContentJob>
{
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        Logger.LogInformation("Index all content job started");

        await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        var content = await Repository.GetAllContentAsync();
        Logger.LogInformation("{count} pieces of content in the repository", content.Count);

        Logger.LogInformation("Index all content job completed");
    }
}