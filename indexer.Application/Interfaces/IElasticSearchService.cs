using System.Threading.Tasks;

namespace indexer.Application.Interfaces;

public interface IElasticSearchService
{
    public bool CheckIndexExists(string name);
    public Task CreateIndexAsync(string name);
    public Task AssignAliasAsync(string alias, string index);

}
