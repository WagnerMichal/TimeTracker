using TimeTracker.DAL;
using Microsoft.EntityFrameworkCore;

namespace TimeTracker.Common.Tests.Factories;

public class DbContextLocalDBTestingFactory : IDbContextFactory<TimeTrackerDbContext>
{
    private readonly string _databaseName;
    private readonly bool _seedTestingData;

    public DbContextLocalDBTestingFactory(string databaseName, bool seedTestingData = false)
    {
        _databaseName = databaseName;
        _seedTestingData = seedTestingData;
    }
    public TimeTrackerDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<TimeTrackerDbContext> builder = new();
        builder.UseSqlServer($"Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog = {_databaseName};MultipleActiveResultSets = True;Integrated Security = True; ");

        // contextOptionsBuilder.LogTo(System.Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
        // builder.EnableSensitiveDataLogging();

        return new TimeTrackerTestingDbContext(builder.Options, _seedTestingData);
    }
}