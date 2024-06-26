using Microsoft.AspNetCore.Mvc;
using Crawlab.DB;
using Crawlab.Model.Models;

namespace Crawlab.API.Controllers;

[ApiController]
[Route("users")]
public class UserController : BaseController<User>
{
    public UserController(ILogger<UserController> logger, CrawlabDbContext dbContext) : base(logger, dbContext)
    {
    }
}