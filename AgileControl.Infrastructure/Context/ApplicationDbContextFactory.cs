using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AgileControl.Infrastructure.Context;

public class ApplicationDbContextFactory
        : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var cfg = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var opts = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(
                cfg.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(
                    typeof(ApplicationDbContext)
                        .Assembly
                        .GetName().Name
                )
            );

        return new ApplicationDbContext(opts.Options);
    }
}