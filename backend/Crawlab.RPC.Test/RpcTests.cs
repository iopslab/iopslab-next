using Crawlab.DB;
using Microsoft.Extensions.Logging;
using Moq;

namespace Crawlab.RPC.Test;

[TestFixture]
public class RpcTests
{
    private CrawlabDbContext _dbContext;
    private RpcClient _client;

    [SetUp]
    public async Task Setup()
    {
        var loggerMock = new Mock<ILogger<RpcClient>>();
        var cancellationTokenSource = new CancellationTokenSource();
        _client = new RpcClient(loggerMock.Object);
        await _client.StartAsync(cancellationTokenSource.Token);

        var factory = new CrawlabDbContextFactory();
        _dbContext = factory.CreateDbContext(Array.Empty<string>());
    }

    [Test]
    public async Task RpcClient_Ping()
    {
        var message = "Ping";
        var resultMessage = await _client.Ping(message);
        Assert.That(resultMessage, Is.EqualTo(message));
    }

    [Test]
    public async Task RpcClient_Register()
    {
        var key = Guid.NewGuid().ToString();
        var node = await _client.Register(key);
        Assert.That(node.Key, Is.EqualTo(key));
        _dbContext.Nodes.Remove(node);
        await _dbContext.SaveChangesAsync();
    }

    [Test]
    public async Task RpcClient_Report()
    {
        var key = Guid.NewGuid().ToString();
        var node = await _client.Register(key);
        var reportedNode = await _client.Report(node.Id);
        Assert.Multiple(() =>
        {
            Assert.That(reportedNode?.Id, Is.EqualTo(node.Id));
            Assert.That(reportedNode?.LastHeartbeat, Is.Not.Null);
        });
        _dbContext.Nodes.Remove(node);
        await _dbContext.SaveChangesAsync();
    }
}