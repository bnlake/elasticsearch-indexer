using System.Threading.Tasks;
using indexer.Application.Interfaces;

namespace indexer.Application.Services;

public class ElasticSearchService : IElasticSearchService
{
    public Task AssignAliasAsync(string alias, string index)
    {
        return Task.CompletedTask;
    }

    public bool CheckIndexExists(string name)
    {
        return true;
    }

    public Task CreateIndexAsync(string name)
    {
        return Task.CompletedTask;
    }
}
