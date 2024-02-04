using System;
using System.Threading;
using System.Threading.Tasks;
using indexer.Application.Interfaces;
using indexer.Domain.Models;
using Microsoft.Extensions.Logging;

namespace indexer.Application.Services;

public class IndexDocumentProcesser(ILogger<IndexDocumentProcesser> Logger) : IIndexDocumentProcesser
{
    public Task AddContent(Content content, CancellationToken cancellationToken)
    {
        var random = new Random();
        bool shouldThrow = random.Next(1, 3) % 2 == 0;
        if (shouldThrow) throw new Exception("Intentional Throw");
        Logger.LogTrace("Indexing content {contentName}", content.Name);
        return Task.CompletedTask;
    }
}
