using Crawlab.Model.Models;

namespace Crawlab.Model.Relations;

public class ScheduleNode : Base.Base
{
    public Schedule? Schedule { get; set; }
    public Node? Node { get; set; }
}