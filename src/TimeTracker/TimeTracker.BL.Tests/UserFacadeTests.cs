using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;
using TimeTracker.Common.Tests;
using TimeTracker.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using TimeTracker.BL.Tests;

namespace TimeTracker.BL.Tests;
public sealed class UserFacadeTests : FacadeTestsBase
{
    private readonly UserFacade _userFacadeSUT;

    private UserDetailModel _user;

    private UserDetailModel _userWithProject;

    public UserFacadeTests(ITestOutputHelper output) : base(output)
    {
        _userFacadeSUT = new UserFacade(UnitOfWorkFactory, UserModelMapper);

        _user = new UserDetailModel()
        {
            ID = Guid.NewGuid(),
            Name = "Ferkos",
            LastName = "Mrkvicka",
            Photo = "https://www.flaticon.com/free-icon/user_1946429",
            Email = "ahoj@gmail.com"

        };

        _userWithProject = new UserDetailModel()
        {
            ID = Guid.NewGuid(),
            Name = "Ferkos",
            LastName = "Mrkvicka",
            Photo = "https://www.flaticon.com/free-icon/user_1946429",
            Email = "ahoj@gmail.com",


        };
    }

    [Fact]
    public async Task CreateUser()
    {
        _user = await _userFacadeSUT.SaveAsync(_user);

        var fromDb = await _userFacadeSUT.GetAsync(_user.ID);
        Assert.NotNull(fromDb);
        Assert.Equal(fromDb.ID, _user.ID);
    }

    [Fact]
    public async Task GetAllUsers()
    {
        _user = await _userFacadeSUT.SaveAsync(_user);

        var users = await _userFacadeSUT.GetAsync();

        Assert.NotEmpty(users);
    }

    [Fact]
    public async Task GetUserById()
    {
        _user = await _userFacadeSUT.SaveAsync(_user);

        var user = await _userFacadeSUT.GetAsync(_user.ID);

        Assert.NotNull(user);
        Assert.Equal(user.ID, _user.ID);
    }
    
    [Fact]
    public async Task UpdateUser()
    {
        _user = await _userFacadeSUT.SaveAsync(_user);
        var fromDbUser = await _userFacadeSUT.GetAsync(_user.ID);

        if (fromDbUser != null) 
        { 
            fromDbUser.Name = "Vladko";
            fromDbUser = await _userFacadeSUT.SaveAsync(fromDbUser);

            Assert.NotEqual(_user.Name, fromDbUser.Name);
            Assert.Equal("Ferkos", _user.Name);
            Assert.Equal("Vladko", fromDbUser.Name);
            Assert.Equal(_user.ID, fromDbUser.ID);
        }
        else
        {
            throw new Exception("Returned null");
        }
    }

    [Fact]
    public async Task DeleteUser()
    {
        _user = await _userFacadeSUT.SaveAsync(_user);

        await _userFacadeSUT.DeleteAsync(_user.ID);

        var fromDbUser = await _userFacadeSUT.GetAsync(_user.ID);

        Assert.Null(fromDbUser);
    }
}
