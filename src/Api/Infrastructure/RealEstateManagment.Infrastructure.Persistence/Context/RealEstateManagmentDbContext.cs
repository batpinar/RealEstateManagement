using Microsoft.EntityFrameworkCore;
using RealEstateManagment.Api.Domain.Models;
using RealEstateManagment.Infrastructure.Persistence.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateManagment.Infrastructure.Persistence.Context;

public class RealEstateManagmentDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = "dbo";
    public RealEstateManagmentDbContext()
    {

    }
    public RealEstateManagmentDbContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<Company> Companies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserProperty> UserProperties { get; set; }
    public DbSet<EmailConfirmation> EmailConfirmations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connStr = "Server=(localdb)\\MSSQLLocalDB;Database=realestatemanage;Trusted_Connection=True";
            optionsBuilder.UseSqlServer(connStr, opt =>
            {
                opt.EnableRetryOnFailure();
            });
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        //modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        //modelBuilder.ApplyConfiguration(new UserPropertyEntityConfiguration());
        //modelBuilder.ApplyConfiguration(new EmailConfirmationEntityConfiguraition());
        //base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        OnBeforeSave();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSave();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void OnBeforeSave()
    {
        var addedEntities = ChangeTracker.Entries()
                                .Where(i => i.State == EntityState.Added)
                                .Select(i => (BaseEntity)i.Entity);
        PrepareAddedEntities(addedEntities);
    }

    private void PrepareAddedEntities(IEnumerable<BaseEntity> baseEntities)
    {
        foreach (var entity in baseEntities)
        {
            if (entity.CreateDate == DateTime.MinValue)
                entity.CreateDate = DateTime.Now;
        }
    }
}
