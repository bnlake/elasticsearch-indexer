using indexer.Application.Interfaces;
using indexer.Application.Models;
using indexer.Domain.Models;
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
        List<(Content, Exception)> failedContent = [];

        Logger.LogTrace("Beginning async foreach over all returned content");
        await foreach (var content in Repository.GetAllContentAsync())
        {
            try
            {
                var task = indexDocumentProcesser.AddContent(content, cancellationToken);
                tasks.Add(task);
            }
            catch (Exception ex)
            {
                failedContent.Add((content, ex));
            }
        }

        await Task.WhenAll(tasks);

        var completedTasksCount = tasks.Count(t => t.IsCompletedSuccessfully);
        Logger.LogInformation("Successful indexing count: {successfulCount}", completedTasksCount);

        var faultedTasksCount = tasks.Count(t => t.IsFaulted);
        Logger.LogInformation("Failed indexing count: {faultedCount}", failedContent.Count);

        Logger.LogInformation("Index all content job completed");
    }
}
