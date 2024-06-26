using Microsoft.AspNetCore.Mvc;
using Crawlab.DB;
using Crawlab.Model.Models;

namespace Crawlab.API.Controllers;

[ApiController]
[Route("nodes")]
public class NodeController : BaseController<Node>
{
    public NodeController(ILogger<NodeController> logger, CrawlabDbContext dbContext) : base(logger, dbContext)
    {
    }

    [HttpGet("{id}/ping")]
    public IResult RegisterNode(int id)
    {
        var existingNode = DbSet.SingleOrDefault(n => n.Id == id);
        return existingNode == null ? Results.NotFound() : Results.Ok();
    }
}