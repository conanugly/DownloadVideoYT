using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DownloadSolution.Data.EF
{
    public class SolutionDbContextFactory : IDesignTimeDbContextFactory<SolutionDbContext>
    {
        public SolutionDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return new SolutionDbContext(new DbContextOptionsBuilder<SolutionDbContext>()
                .UseSqlServer(configurationRoot.GetConnectionString("DefaultConnection")).Options);
        }
    }
}
