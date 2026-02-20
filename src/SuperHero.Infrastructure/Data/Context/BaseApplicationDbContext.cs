using System.Reflection;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Interfaces;
using SuperHero.Infrastructure.Extensions;

namespace SuperHero.Infrastructure.Data.Context;

public class BaseApplicationDbContext : DbContext, IUnitOfWork
{
    public BaseApplicationDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Heroi> Herois { get; set; }
    public DbSet<Superpoder> Superpoderes { get; set; }
    public DbSet<HeroisSuperpoderes> HeroiSuperpoderes { get; set; }
    
    public async Task<bool> Commit() => await SaveChangesAsync() > 0;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        ApplyConfigurations(modelBuilder);
        
        base.OnModelCreating(modelBuilder);
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        ApplyTrackingChanges();
        return base.SaveChangesAsync(cancellationToken);
    }
    
    private void ApplyTrackingChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is ITracking && e.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntry in entries)
        {
            ((ITracking)entityEntry.Entity).AtualizadoEm = DateTime.Now;

            if (entityEntry.State != EntityState.Added)
                continue;
            
            ((ITracking)entityEntry.Entity).CriadoEm = DateTime.Now;
        }
    }
    
    private static void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();

        modelBuilder.ApplyEntityConfiguration();
        modelBuilder.ApplyTrackingConfiguration();
    }
}