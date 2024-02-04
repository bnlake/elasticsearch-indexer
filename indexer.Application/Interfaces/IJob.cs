using System.Threading;
using System.Threading.Tasks;

namespace indexer.Application.Interfaces;

public interface IJob<T> where T : class
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}
