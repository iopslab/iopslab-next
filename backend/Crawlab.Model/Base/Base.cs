using Microsoft.EntityFrameworkCore;

namespace Crawlab.Model.Base;

[PrimaryKey(nameof(Id))]
public class Base
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}