using Microsoft.AspNetCore.Mvc;
using Crawlab.DB;
using Crawlab.Model.Models;

namespace Crawlab.API.Controllers;

[ApiController]
[Route("spiders")]
public class SpiderController : BaseController<Spider>
{
    public SpiderController(ILogger<SpiderController> logger, CrawlabDbContext dbContext) : base(logger, dbContext)
    {
    }
}