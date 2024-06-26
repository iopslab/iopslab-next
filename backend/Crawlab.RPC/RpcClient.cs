using Crawlab.Model.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Task = System.Threading.Tasks.Task;

namespace Crawlab.RPC;

public class RpcClient : IRpcClient
{
    private readonly ILogger<RpcClient> _logger;
    public readonly HubConnection Connection;

    public RpcClient(ILogger<RpcClient> logger)
    {
        _logger = logger;
        Connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5129/rpc")
            .Build();

        Connection.Closed += async (error) =>
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await Connection.StartAsync();
        };

        Connection.On<string>("Pong", Pong);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (true)
        {
            try
            {
                await Connection.StartAsync(cancellationToken);
                _logger.LogInformation("RPC client started");
                break;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error when starting RPC client");
                await Task.Delay(10 * 1000);
            }
        }
    }

    public async Task StopAsync()
    {
        await Connection.DisposeAsync();
        _logger.LogInformation("RPC client stopped");
    }

    public async Task<string> Ping(string message)
    {
        return await Connection.InvokeAsync<string>("Ping", message);
    }

    public async Task Pong(string message)
    {
        _logger.LogInformation("Pong: {Message}", message);
    }

    public async Task<Node> Register(string? key)
    {
        return await Connection.InvokeAsync<Node>("Register", key);
    }

    public async Task<Node?> Report(int nodeId)
    {
        return await Connection.InvokeAsync<Node>("Report", nodeId);
    }
}