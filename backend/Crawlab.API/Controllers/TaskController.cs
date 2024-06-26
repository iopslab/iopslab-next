using Microsoft.AspNetCore.Mvc;
using Crawlab.DB;
using Task = Crawlab.Model.Models.Task;

namespace Crawlab.API.Controllers;

[ApiController]
[Route("tasks")]
public class TaskController : BaseController<Task>
{
    public TaskController(ILogger<TaskController> logger, CrawlabDbContext dbContext) : base(logger, dbContext)
    {
    }
}