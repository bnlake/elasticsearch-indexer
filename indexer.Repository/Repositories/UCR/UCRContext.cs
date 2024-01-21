using indexer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace indexer.Repository.Repositories.UCR;

public class UCRContext(DbContextOptions<UCRContext> options) : DbContext(options)
{
    public DbSet<Content> Contents { get; set; }
}