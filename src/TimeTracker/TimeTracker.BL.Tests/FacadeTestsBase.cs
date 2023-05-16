using TimeTracker.BL.Mappers;
using TimeTracker.Common.Tests;
using TimeTracker.Common.Tests.Factories;
using TimeTracker.DAL;
using TimeTracker.DAL.Mappers;
using TimeTracker.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace TimeTracker.BL.Tests;

public class FacadeTestsBase : IAsyncLifetime
{
    protected FacadeTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        // DbContextFactory = new DbContextTestingInMemoryFactory(GetType().Name, seedTestingData: true);
        DbContextFactory = new DbContextLocalDBTestingFactory(GetType().FullName!, seedTestingData: true);
        // DbContextFactory = new DbContextSqLiteTestingFactory(GetType().FullName!, seedTestingData: true);

        UserInProjectModelMapper = new UserInProjectModelMapper();
        ProjectModelMapper = new ProjectModelMapper(UserInProjectModelMapper);
        ActivityModelMapper = new ActivityModelMapper();
        UserModelMapper = new UserModelMapper(UserInProjectModelMapper, ActivityModelMapper);

        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);
    }

    protected IDbContextFactory<TimeTrackerDbContext> DbContextFactory { get; }

    protected IActivityModelMapper ActivityModelMapper { get; }
    protected IProjectModelMapper ProjectModelMapper { get; }
    protected IUserModelMapper UserModelMapper { get; }
    protected IUserInProjectModelMapper UserInProjectModelMapper { get; }
    protected UnitOfWorkFactory UnitOfWorkFactory { get; }

    public async Task InitializeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
        await dbx.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
    }
}
