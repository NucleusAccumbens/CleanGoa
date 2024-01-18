using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence;

public class CleanGoaBotDbContext : DbContext, IBotDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public DbSet<TlgUser> TlgUsers => Set<TlgUser>();

    public DbSet<Event> Events=> Set<Event>();

    public CleanGoaBotDbContext(DbContextOptions<CleanGoaBotDbContext> options, 
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<TlgUser>().HasData(new TlgUser[]
        {
            new TlgUser
            {
                Id = 1,
                ChatId = 444343256,
                Username = "noncredist",
                IsKicked = false,
                IsAdmin = true,
                IsDeleted = false,
                Language = Domain.Enums.Language.Russian
            }
        });

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public async Task SaveChangesAsync()
    {
        await base.SaveChangesAsync();
    }
}
