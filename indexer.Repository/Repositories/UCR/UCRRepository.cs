using System.Collections.Generic;
using System.Threading.Tasks;
using indexer.Domain.Models;
using indexer.Repository.Repositories.UCR;

namespace indexer.Repository.Repositories;

public class UCRRepository : IContentRepository
{
    private readonly UCRContext context;

    public UCRRepository(UCRContext context)
    {
        this.context = context;
    }

    public Task<IEnumerable<Content>> GetAllContentAsync()
    {
        return Task.FromResult(new List<Content>() as IEnumerable<Content>);
    }

    public async Task<Content> GetContentAsync(int id)
    {
        return await context.Contents.FindAsync(id) ?? throw new KeyNotFoundException();
    }
}