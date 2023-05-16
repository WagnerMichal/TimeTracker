using TimeTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace TimeTracker.Common.Tests.Seeds;

public static class UserSeedsCommon
{
    public static readonly UserEntity EmptyUserEntity = new()
    {
        ID = default,
        Name = default!,
        LastName = default!,
        Email = default,
        Photo = default!,
    };

    public static readonly UserEntity UserEntity = new()
    {
        ID = Guid.Parse(input: "06a8a2cf-ea03-4095-a3e4-aa0291fe9c75"),
        Name = "Karel",
        LastName = "Roman",
        Email = "ppppp@fdf.cz",
        Photo = "dffdfd",
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly UserEntity UserUpdate = UserEntity with { ID = Guid.Parse("143332B9-080E-4953-AEA5-BEF64679B052") };
    public static readonly UserEntity UserDelete = UserEntity with { ID = Guid.Parse("274D0CC9-A948-4818-AADB-A8B4C0506619") };

    public static UserEntity UserEntity1 = new()
    {
        ID = Guid.Parse(input: "df935095-8709-4040-a2bb-b6f97cb416dc"),
        Name = "Petr",
        LastName = "Svoboda",
        Email = null,
        Photo = "dffdfgfgdgfd"
    };

    public static UserEntity UserEntity2 = new()
    {
        ID = Guid.Parse(input: "23b3902d-7d4f-4213-9cf0-112348f56238"),
        Name = "Lopat",
        LastName = "Romanov",
        Email = null,
        Photo = "dffddddddddddfd"
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            UserEntity1,
            UserEntity2,
            UserEntity,
            UserUpdate,
            UserDelete);
    }
}