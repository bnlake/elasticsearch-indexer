using System.Collections.Generic;
using System.Threading.Tasks;
using indexer.Domain.Models;
using indexer.Repository.Repositories.UCR;

namespace indexer.Repository.Repositories;

public class UCRRepository(UCRContext Context) : IContentRepository
{
    public IAsyncEnumerable<Content> GetAllContentAsync()
    {
        return Context.Contents.AsAsyncEnumerable();
    }

    public async Task<Content> GetContentAsync(int id)
    {
        return await Context.Contents.FindAsync(id) ?? throw new KeyNotFoundException();
    }
}
