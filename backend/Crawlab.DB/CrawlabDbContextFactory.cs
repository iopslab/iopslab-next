using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Crawlab.DB;

public class CrawlabDbContextFactory : IDesignTimeDbContextFactory<CrawlabDbContext>
{
    public CrawlabDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CrawlabDbContext>();

        return new CrawlabDbContext(optionsBuilder.Options);
    }
}