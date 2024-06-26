using Crawlab.Model.Models;
using Task = System.Threading.Tasks.Task;

namespace Crawlab.RPC;

public interface IRpcClient
{
    public Task<string> Ping(string message);
    public Task Pong(string message);
    public Task<Node> Register(string? key);
    public Task<Node?> Report(int nodeId);
}