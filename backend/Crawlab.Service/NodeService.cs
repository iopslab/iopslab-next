using Crawlab.Constant;
using Crawlab.DB;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Crawlab.Service;

public class NodeService : BackgroundService
{
    private readonly ILogger<NodeService> _logger;
    private readonly CrawlabDbContextFactory _factory;
    private const int OnlineIntervalSeconds = 10;

    public NodeService(ILogger<NodeService> logger, CrawlabDbContextFactory factory)
    {
        _logger = logger;
        _factory = factory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Node service started");

        while (!stoppingToken.IsCancellationRequested)
        {
            await using (var dbService = _factory.CreateDbContext(Array.Empty<string>()))
            {
                foreach (var node in dbService.Nodes.Where(n => n.Status == NodeStatus.Online))
                {
                    if (node.LastHeartbeat == null ||
                        (DateTime.Now - node.LastHeartbeat.Value).Seconds > OnlineIntervalSeconds)
                    {
                        node.Status = NodeStatus.Offline;
                        _logger.LogInformation("Node {NodeName} (ID: {NodeId}) is offline", node.Name, node.Id);
                    }
                    else
                    {
                        node.Status = NodeStatus.Online;
                        _logger.LogInformation("Node {NodeName} (ID: {NodeId}) is online", node.Name, node.Id);
                    }
                }

                await dbService.SaveChangesAsync(stoppingToken);
            }

            await Task.Delay(OnlineIntervalSeconds * 1000, stoppingToken);
        }

        _logger.LogInformation("Node service stopped");
    }
}