using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace E_invocing.Persistence
{
    public class EInvocingDbContextFactory : IDesignTimeDbContextFactory<E_invocingDbContext>
    {
        public E_invocingDbContext CreateDbContext(string[] args)
        {
            // Build configuration to read connection string
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json") // read connection string
                .AddUserSecrets<EInvocingDbContextFactory>(optional: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<E_invocingDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new E_invocingDbContext(optionsBuilder.Options);
        }
    }
}
