using Crawlab.Constant;
using Crawlab.Model.Base;
using Crawlab.Model.Relations;

namespace Crawlab.Model.Models;

public class Schedule : BaseModel
{
    public string Cron { get; set; } = string.Empty;
    public string Mode { get; set; } = TaskMode.Random;
    public Spider Spider { get; set; } = new();

    public List<ScheduleNode> ScheduleNodes { get; set; } = new();
    public List<Node> Nodes { get; set; } = new();
}