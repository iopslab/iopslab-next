using Crawlab.Model.Base;
using Crawlab.Model.Models;
using Crawlab.Model.Relations;
using Microsoft.EntityFrameworkCore;
using Task = Crawlab.Model.Models.Task;

namespace Crawlab.DB;

public class CrawlabDbContext : DbContext
{
    private readonly string _connectionString = Environment.GetEnvironmentVariable("CRAWLAB_MYSQL_CONNECTION") ??
                                                "server=localhost; database=crawlab; user=crawlab; password=crawlab;";

    public DbSet<Node> Nodes { get; set; }
    public DbSet<Spider> Spiders { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<SpiderNode> SpiderNodes { get; set; }
    public DbSet<ScheduleNode> ScheduleNodes { get; set; }

    public CrawlabDbContext(DbContextOptions<CrawlabDbContext> options) : base(options)
    {
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options
            .UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString))
            // The following three options help with debugging, but should
            // be changed or removed for production.
            // .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();

    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is BaseModel && x.State == EntityState.Modified);

        foreach (var entity in entities)
        {
            ((BaseModel)entity.Entity).UpdatedAt = DateTime.Now;
        }
    }
}