using Crawlab.Model.Base;
using Crawlab.Model.Models;

namespace Crawlab.Model.Relations;

public class SpiderNode : Base.Base
{
    public Spider? Spider { get; set; }
    public Node? Node { get; set; }
}