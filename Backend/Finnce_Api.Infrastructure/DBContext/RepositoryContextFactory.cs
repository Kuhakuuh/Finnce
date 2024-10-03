
namespace Finnce_Api.Infrastructure.DBContext;

public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
{


    public RepositoryContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();
        var builder = new DbContextOptionsBuilder<RepositoryContext>()
       .UseSqlServer(configuration.GetConnectionString("sqlConnection"),
       b => b.MigrationsAssembly("Finnce_Api.Infrastructure"));


        return new RepositoryContext(builder.Options);
    }
}
