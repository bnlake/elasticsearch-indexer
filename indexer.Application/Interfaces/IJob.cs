using System.Threading;
using System.Threading.Tasks;

namespace indexer.Application.Interfaces;

public interface IJob
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}