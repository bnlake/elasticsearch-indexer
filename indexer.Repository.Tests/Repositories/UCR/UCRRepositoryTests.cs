using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using indexer.Domain.Models;
using indexer.Repository.Repositories;
using indexer.Repository.Repositories.UCR;
using Microsoft.EntityFrameworkCore;

namespace indexer.Repository.Tests.UCR;

[TestFixture]
public class UCRRepositoryTests : IDisposable
{
    private UCRContext context;
    private UCRRepository repository;

    [OneTimeSetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<UCRContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;
        context = new UCRContext(options);
        context.Database.EnsureCreated();

        repository = new(context);
    }

    [OneTimeTearDown]
    public void Dispose()
    {
        context.Dispose();
    }

    [TestCase(1, "Headache")]
    public async Task GetKnownContent_Succeeds(int id, string name)
    {
        context.Contents.Add(new Content { Id = id, Name = name });
        context.SaveChanges();

        var result = await repository.GetContentAsync(id);

        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.Name, Is.EqualTo(name));
        });
    }

    [Test]
    public void GetUnknownContent_ThrowsKeyNotFoundException()
    {
        var result = Assert.ThrowsAsync<KeyNotFoundException>(async () => await repository.GetContentAsync(948));

        Assert.That(result, Is.Not.Null);
    }
}