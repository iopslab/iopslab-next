using Crawlab.Constant;
using Crawlab.DB;
using Crawlab.Model.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Task = System.Threading.Tasks.Task;

namespace Crawlab.RPC;

public class RpcServer : Hub<IRpcClient>
{
    private readonly ILogger<RpcServer> _logger;
    private readonly CrawlabDbContext _dbContext;

    public RpcServer(ILogger<RpcServer> logger, CrawlabDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation("SignalR connected: {ContextConnectionId}", Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("SignalR disconnected: {ContextConnectionId}", Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }

    public async Task<string> Ping(string message)
    {
        _logger.LogInformation("Ping: {ContextConnectionId}", Context.ConnectionId);
        await Clients.Client(Context.ConnectionId).Pong(message);
        return message;
    }

    public async Task<Node> Register(string key)
    {
        var node = _dbContext.Nodes.SingleOrDefault(x => x.Key == key);
        if (node == null)
        {
            node = new Node
            {
                Key = key,
                Name = key,
            };
            await _dbContext.Nodes.AddAsync(node);
            await _dbContext.SaveChangesAsync();
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, node.Key);
        return node;
    }

    public async Task<Node?> Report(int nodeId)
    {
        var node = _dbContext.Nodes.SingleOrDefault(x => x.Id == nodeId);
        if (node == null)
        {
            return null;
        }

        node.Status = NodeStatus.Online;
        node.LastHeartbeat = DateTime.Now;
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Report: {ContextConnectionId}", Context.ConnectionId);

        return node;
    }
}