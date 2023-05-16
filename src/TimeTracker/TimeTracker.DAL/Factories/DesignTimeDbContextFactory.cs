using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TimeTracker.DAL.Factories;

/// <summary>
///     EF Core CLI migration generation uses this DbContext to create model and migration
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TimeTrackerDbContext>
{
    public TimeTrackerDbContext CreateDbContext(string[] args) 
    {
        var builder = new DbContextOptionsBuilder<TimeTrackerDbContext>();
        builder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB; Initial Catalog = TimeTracker; MultipleActiveResultSets = True; Integrated Security = True; Encrypt=False; TrustServerCertificate = True;");
        return new TimeTrackerDbContext(builder.Options);
    }
}