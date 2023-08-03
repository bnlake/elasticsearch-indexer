using indexer.Domain.Models;
using indexer.Repository.Repositories.UCR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace indexer.Repository.Tests;

public class UCRContextTests
{
    private UCRContext context;

    [SetUp]
    public async Task Initialize()
    {
        var options = new DbContextOptionsBuilder<UCRContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;
        context = new UCRContext(options);
        context.Database.EnsureCreated();

        await context.Contents.AddAsync(new Content { Id = 1, Name = "Headache" });
        await context.Contents.AddAsync(new Content { Id = 2, Name = "Anxiety" });
        await context.SaveChangesAsync();
    }

    [TestCase(1, "Headache")]
    [TestCase(2, "Anxiety")]
    public async Task CanGetSingleContent(int id, string title)
    {
        var content = await context.Contents.FirstOrDefaultAsync(c => c.Id.Equals(id));

        Assert.That(content, Is.Not.Null);
        Assert.That(content.Name, Is.EqualTo(title));
    }

    [Test]
    public async Task CanGetAllContent()
    {
        var count = await context.Contents.CountAsync();

        Assert.That(count, Is.EqualTo(2));
    }

    [TearDown]
    public void Dispose()
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}