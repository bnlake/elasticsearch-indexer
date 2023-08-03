using System.Collections.Generic;
using System.Threading.Tasks;
using indexer.Domain.Models;

namespace indexer.Repository;

public interface IContentRepository
{
    Task<Content> GetContentAsync(int id);
    Task<IEnumerable<Content>> GetAllContentAsync();
}