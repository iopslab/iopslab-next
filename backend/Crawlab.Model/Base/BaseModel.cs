using Microsoft.EntityFrameworkCore;

namespace Crawlab.Model.Base;

public abstract class BaseModel : Base
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}