using indexer.Application.Interfaces;
using indexer.Application.Models;
using indexer.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace indexer.Application.Jobs;

public class IndexAllContentJob(IContentRepository Repository,
    IElasticSearchService ESService,
    IIndexDocumentProcesser indexDocumentProcesser,
    IOptionsSnapshot<IndexOptions> indexOptions,
    ILogger<IndexAllContentJob> Logger) : IJob<IndexAllContentJob>
{
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        Logger.LogInformation("Index all content job started");

        var indexName = indexOptions.Value.Name;
        if (!ESService.CheckIndexExists(indexName)) await ESService.CreateIndexAsync(indexName);

        List<Task> tasks = [];

        Logger.LogTrace("Beginning async foreach over all returned content");
        await foreach (var content in Repository.GetAllContentAsync())
        {
            tasks.Add(indexDocumentProcesser.AddContent(content, cancellationToken));
        }

        await Task.WhenAll(tasks);
        Logger.LogInformation("Total content processed: {totalCount}", tasks.Count);

        var completedTasksCount = tasks.Count(t => t.IsCompletedSuccessfully);
        Logger.LogInformation("Successful indexing count: {successfulCount}", completedTasksCount);

        var faultedTasksCount = tasks.Count(t => t.IsFaulted);
        Logger.LogInformation("Failed indexing count: {faultedCount}", faultedTasksCount);

        Logger.LogInformation("Index all content job completed");
    }
}
