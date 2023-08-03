using System.Collections.Generic;
using indexer.Domain.Models;

namespace indexer.Repository.Repositories;

public class UCRRepository : IContentRepository
{
    public ICollection<Content> GetAllContent()
    {
        throw new System.NotImplementedException();
    }

    public Content GetContent(string id)
    {
        throw new System.NotImplementedException();
    }
}