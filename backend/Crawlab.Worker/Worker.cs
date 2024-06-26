using Crawlab.Model.Models;
using Crawlab.RPC;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Task = System.Threading.Tasks.Task;

namespace Crawlab.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly RpcClient _rpcClient;
    private Node? _node;

    public Worker(ILogger<Worker> logger, RpcClient rpcClient)
    {
        _logger = logger;
        _rpcClient = rpcClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _rpcClient.StartAsync(stoppingToken);
        _node = await _rpcClient.Register("worker");
        _logger.LogInformation("Worker started");

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {Time}", DateTimeOffset.Now);
            if (_node != null)
            {
                try
                {
                    _node = await _rpcClient.Report(_node.Id);
                }
                catch (Exception e)
                {
                    if (_rpcClient.Connection.State == HubConnectionState.Disconnected)
                    {
                        await _rpcClient.StartAsync(stoppingToken);
                    }
                }
            }

            await Task.Delay(5 * 1000, stoppingToken);
        }

        await _rpcClient.StopAsync();
        _logger.LogInformation("Worker stopped");
    }
}