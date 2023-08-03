using indexer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace indexer.Repository.Repositories.UCR;

public class UCRContext : DbContext
{
    public UCRContext(DbContextOptions<UCRContext> options) : base(options) { }

    public DbSet<Content> Contents { get; set; }
}