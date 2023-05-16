using TimeTracker.Common.Tests;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TimeTracker.DAL.Entities;
using Xunit.Abstractions;

namespace TimeTracker.DAL.Tests;

public class UserTests : DbContextTestsBase
{
    public UserTests(ITestOutputHelper output) : base(output)
    {
    }
    [Fact]
    public async Task NewUser_Add_Added()
    {
        var user = new UserEntity()
        {
            ID = Guid.Parse("7c6cdd3e-481d-427a-a971-ae306aba8c95"),
            Name = "Michal",
            LastName = "Lopata",
            Email = "lopata@gmail.com",
            Photo = "https://www.flaticon.com/free-icon/user_1946429"
        };
        TimeTrackerDbContextSUT.Users.Add(user);
        TimeTrackerDbContextSUT.SaveChanges();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Users
            .SingleAsync(i => i.ID == user.ID);
        DeepAssert.Equal(user, actualEntity);
    }
}