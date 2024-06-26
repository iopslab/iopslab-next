using Microsoft.AspNetCore.Mvc;
using Crawlab.DB;
using Crawlab.Model.Models;
using Crawlab.RPC;
using Microsoft.AspNetCore.SignalR;

namespace Crawlab.API.Controllers;

[ApiController]
[Route("rpc-test")]
public class RpcTestController
{
    private readonly ILogger<RpcTestController> _logger;
    private readonly IHubContext<RpcServer, IRpcClient> _rpcServer;

    public RpcTestController(ILogger<RpcTestController> logger, IHubContext<RpcServer, IRpcClient> rpcServer)
    {
        _logger = logger;
        _rpcServer = rpcServer;
    }

    [HttpGet]
    public async Task<string> Get(string key)
    {
        _logger.LogInformation("Ping: {Key}", key);
        await _rpcServer.Clients.Group(key).Pong("Pong");
        return "success";
    }
}