using Microsoft.EntityFrameworkCore;
using Npgsql;
using Shared.Databases.Entities;
using Shared.Models.Configurations;

namespace Shared.Databases.DbContexts;

public class AuthenticationDbContext: DbContext
{
    private AuthenticationConfiguration authenticationConfiguration;
    public DbSet<User> Users { get; set; }
    public DbSet<AuthCode> AuthCodes { get; set; }

    public AuthenticationDbContext(
        AuthenticationConfiguration configuration,
        DbContextOptions<AuthenticationDbContext> options
    ): base(options) {
        this.authenticationConfiguration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { optionsBuilder.UseNpgsql(authenticationConfiguration.ConnectionString); }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("user_id");
            entity.Property(e => e.Email).HasColumnName("email").IsRequired();
            entity.Property(a => a.Name).HasColumnName("name").IsRequired();
            entity.Property(a => a.Password).HasColumnName("passphrase").IsRequired();
            entity.Property(a => a.Active).HasColumnName("active").IsRequired();
            entity.Property(a => a.ForcePasswordReset).HasColumnName("force_password_reset").IsRequired();
            entity.Property(a => a.CreatedAt).HasColumnName("created_at");
            entity.Property(a => a.DeletedAt).HasColumnName("deleted_at");
            entity.HasMany(a => a.AuthCodes)
                .WithOne(a => a.User)
                .HasForeignKey("user_id");
        });
        
        modelBuilder.Entity<AuthCode>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("code_id");
            entity.Property(e => e.Code).HasColumnName("code").IsRequired();
            entity.Property(a => a.ExpireIn).HasColumnName("expire_in").IsRequired();
            entity.Property(a => a.CreatedAt).HasColumnName("created_at");
            entity.HasOne(a => a.User)
                .WithMany(a => a.AuthCodes)
                .HasForeignKey("user_id");
        });
    }
}