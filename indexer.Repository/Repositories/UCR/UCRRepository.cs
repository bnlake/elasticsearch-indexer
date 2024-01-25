using System.Collections.Generic;
using System.Threading.Tasks;
using indexer.Domain.Models;
using indexer.Repository.Repositories.UCR;
using Microsoft.EntityFrameworkCore;

namespace indexer.Repository.Repositories;

public class UCRRepository(UCRContext Context) : IContentRepository
{
    public Task<List<Content>> GetAllContentAsync()
    {
        return Context.Contents.ToListAsync();
    }

    public async Task<Content> GetContentAsync(int id)
    {
        return await Context.Contents.FindAsync(id) ?? throw new KeyNotFoundException();
    }
}