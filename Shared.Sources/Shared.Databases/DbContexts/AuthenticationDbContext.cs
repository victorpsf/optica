using Microsoft.EntityFrameworkCore;
using Shared.Databases.Entities;
using Shared.Databases.Interceptors;
using Shared.Interfaces.Configurations;

namespace Shared.Databases.DbContexts;

public class AuthenticationDbContext: DbContext
{
    private IAuthenticationConfiguration authenticationConfiguration;
    public DbSet<User> Users { get; set; }
    public DbSet<AuthCode> AuthCodes { get; set; }

    public AuthenticationDbContext(
        IAuthenticationConfiguration configuration,
        DbContextOptions<AuthenticationDbContext> options
    ): base(options) {
        this.authenticationConfiguration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(authenticationConfiguration.ConnectionString);
//#if DEBUG
//        optionsBuilder.AddInterceptors(new UtcSaveChangesInterceptor());
//#endif
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.Active).IsRequired().HasDefaultValue(true);
            entity.Property(e => e.ForcePasswordReset).IsRequired().HasDefaultValue(false);
            entity.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("now()");
            entity.Property(e => e.DeletedAt);

            entity.HasMany(e => e.AuthCodes)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);

            entity.HasMany(e => e.Roles)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);

            entity.HasMany(e => e.UserEnterprises)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);
        });

        modelBuilder.Entity<Enterprise>(entity =>
        {
            entity.HasKey(e => e.EnterpriseId);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Active).IsRequired().HasDefaultValue(true);
            entity.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("now()");
            entity.Property(e => e.DeletedAt);

            entity.HasMany(e => e.UserEnterprises)
                .WithOne(e => e.Enterprise)
                .HasForeignKey(e => e.EntepriseId);
        });

        modelBuilder.Entity<AuthCode>(entity =>
        {
            entity.HasKey(e => e.CodeId);
            entity.Property(e => e.CodeId);
            entity.Property(e => e.Code).IsRequired().HasMaxLength(9);
            entity.Property(e => e.ExpireIn).IsRequired();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

            entity.HasOne(e => e.User)
                .WithMany(e => e.AuthCodes)
                .HasForeignKey(e => e.UserId);
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId);
            entity.Property(e => e.PermissionId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(500);

            entity.HasMany(e => e.RolePermissions)
                .WithOne(e => e.Permission)
                .HasForeignKey(e => e.PermissionId);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId);
            entity.Property(e => e.RoleId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(500);

            entity.HasMany(e => e.RolePermissions)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleId);

            entity.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleId);
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => new { e.PermissionId, e.RoleId });

            entity.HasOne(e => e.Permission)
                .WithMany(e => e.RolePermissions)
                .HasForeignKey(e => e.PermissionId);

            entity.HasOne(e => e.Role)
                .WithMany(e => e.RolePermissions)
                .HasForeignKey(e => e.RoleId);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId });

            entity.HasOne(e => e.User)
                .WithMany(e => e.Roles)
                .HasForeignKey(e => e.UserId);

            entity.HasOne(e => e.Role)
                .WithMany(e => e.UserRoles)
                .HasForeignKey(e => e.RoleId);
        });

        modelBuilder.Entity<UserEnterprise>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.EntepriseId });

            entity.HasOne(e => e.User)
                .WithMany(e => e.UserEnterprises)
                .HasForeignKey(e => e.UserId);

            entity.HasOne(e => e.Enterprise)
                .WithMany(e => e.UserEnterprises)
                .HasForeignKey(e => e.EntepriseId);
        });
    }
}