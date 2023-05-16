using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Seeds;
using Microsoft.EntityFrameworkCore;

namespace TimeTracker.DAL;

public class TimeTrackerDbContext : DbContext
{
    private readonly bool _seedDemoData;

    public TimeTrackerDbContext(DbContextOptions contextOptions, bool seedDemoData = false) :
        base(contextOptions) =>
        _seedDemoData = seedDemoData;

   
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();
    public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();
    public DbSet<UserInProjectEntity> UsersInProjects => Set<UserInProjectEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserEntity>()
            .HasMany(u => u.Activities);

        modelBuilder.Entity<ProjectEntity>()
            .HasMany(p => p.Activities);

        modelBuilder.Entity<UserInProjectEntity>()
            .HasKey(up => new { up.UserID, up.ProjectID });

        modelBuilder.Entity<UserInProjectEntity>()
            .HasOne(up => up.User)
            .WithMany(u => u.Projects)
            .HasForeignKey(up => up.UserID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserInProjectEntity>()
            .HasOne(up => up.Project)
            .WithMany(p => p.Users)
            .HasForeignKey(up => up.ProjectID)
            .OnDelete(DeleteBehavior.Cascade);



        /*if (_SeedTimeTrackerData)
        {
            ActivitySeeds.Seed(modelBuilder);
            ProjectSeeds.Seed(modelBuilder);
            UserInProjectSeeds.Seed(modelBuilder);
            UserSeeds.Seed(modelBuilder);
        }*/
    }
}