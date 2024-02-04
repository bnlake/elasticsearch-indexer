using System.Threading;
using System.Threading.Tasks;
using indexer.Domain.Models;

namespace indexer.Application.Interfaces;

public interface IIndexDocumentProcesser
{
    public Task AddContent(Content content, CancellationToken cancellationToken);
}
