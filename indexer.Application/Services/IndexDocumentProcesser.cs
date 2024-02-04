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
        Logger.LogTrace("Indexing content {contentName}", content.Name);
        return Task.CompletedTask;
    }
}
