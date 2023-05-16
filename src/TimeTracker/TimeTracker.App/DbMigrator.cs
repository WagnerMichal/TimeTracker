using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TimeTracker.App.Options;
using TimeTracker.DAL;

namespace TimeTracker.App;

interface IDbMigrator
{
    public void Migrate();
    public Task MigrateAsync(CancellationToken cancellationToken);
}

public class NoneDbMigrator : IDbMigrator
{
    public void Migrate() { }

    public Task MigrateAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}

public class SqlServerDbMigrator : IDbMigrator
{
    private readonly IDbContextFactory<TimeTrackerDbContext> _dbContextFactory;
    private readonly LocalDbOptions _localDbOptions;

    public SqlServerDbMigrator(IDbContextFactory<TimeTrackerDbContext> dbContextFactory, DALOptions dalOptions)
    {
        _dbContextFactory = dbContextFactory;
        _localDbOptions = dalOptions.LocalDb ?? throw new ArgumentNullException(nameof(dalOptions), $@"{nameof(DALOptions.LocalDb)} are not set");
    }

    public void Migrate() => MigrateAsync(CancellationToken.None).GetAwaiter().GetResult();

    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        await using TimeTrackerDbContext dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        if (_localDbOptions.RecreateDatabaseEachTime)
        {
            await dbContext.Database.EnsureDeletedAsync(cancellationToken);
        }

        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
    }
}

public class SqliteDbMigrator : IDbMigrator
{
    private readonly IDbContextFactory<TimeTrackerDbContext> _dbContextFactory;
    private readonly SqliteOptions _sqliteOptions;

    public SqliteDbMigrator(IDbContextFactory<TimeTrackerDbContext> dbContextFactory, DALOptions dalOptions)
    {
        _dbContextFactory = dbContextFactory;
        _sqliteOptions = dalOptions.Sqlite ?? throw new ArgumentNullException(nameof(dalOptions), $@"{nameof(DALOptions.Sqlite)} are not set");
    }

    public void Migrate() => MigrateAsync(CancellationToken.None).GetAwaiter().GetResult();

    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        await using TimeTrackerDbContext dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        if (_sqliteOptions.RecreateDatabaseEachTime)
        {
            await dbContext.Database.EnsureDeletedAsync(cancellationToken);
        }

        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
    }
}

