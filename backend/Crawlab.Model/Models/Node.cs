using Crawlab.Constant;
using Crawlab.Model.Base;
using Microsoft.EntityFrameworkCore;

namespace Crawlab.Model.Models;

[Index(nameof(Key), IsUnique = true)]
[Index(nameof(Status))]
public class Node : BaseModel
{
    public string Key { get; set; } = Guid.NewGuid().ToString();
    public string Status { get; set; } = NodeStatus.Online;
    public bool Enabled { get; set; } = true;
    public int ActiveRunners { get; set; }
    public int MaxRunners { get; set; } = 32;
    public DateTime? LastHeartbeat { get; set; }
}