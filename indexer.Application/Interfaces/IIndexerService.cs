using System.Threading.Tasks;

namespace indexer.Application.Interfaces;

public interface IIndexerService
{
    public Task IndexSingleContent(int id);
}
