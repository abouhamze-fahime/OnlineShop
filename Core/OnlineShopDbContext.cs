using Core.Entities;
using Core.FluentAPIConfigurations;
using Microsoft.EntityFrameworkCore;

public class OnlineShopDbContext : DbContext
{
    public OnlineShopDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserRefreshToken> UserRefreshTokens => Set<UserRefreshToken>();
    public DbSet<Application> Applications => Set<Application>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<ApplicationPermission> ApplicationPermissions => Set<ApplicationPermission>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<RoleApplicationPermission> RoleApplications => Set<RoleApplicationPermission>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserRefreshTokenEntityConfiguration());
    }
}