using System.Collections.Generic;
using indexer.Domain.Models;

namespace indexer.Repository;

public interface IContentRepository
{
    Content GetContent(string id);
    ICollection<Content> GetAllContent();
}