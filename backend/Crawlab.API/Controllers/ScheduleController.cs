using Microsoft.AspNetCore.Mvc;
using Crawlab.DB;
using Crawlab.Model.Models;

namespace Crawlab.API.Controllers;

[ApiController]
[Route("schedules")]
public class ScheduleController : BaseController<Schedule>
{
    public ScheduleController(ILogger<ScheduleController> logger, CrawlabDbContext dbContext) : base(logger, dbContext)
    {
    }
}