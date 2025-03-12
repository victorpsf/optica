using Microsoft.EntityFrameworkCore;
using Shared.Databases.Entities;
using Shared.Databases.Interceptors;
using Shared.Interfaces.Configurations;

namespace Shared.Databases.DbContexts;

public class PersonDbContext : DbContext
{
    private IPersonalConfiguration personalConfiguration;
    public DbSet<Person> Persons { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Contact> Contacts { get; set; }

    public PersonDbContext(
        IPersonalConfiguration configuration,
        DbContextOptions<PersonDbContext> options
    ) : base(options)
    {
        this.personalConfiguration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(personalConfiguration.ConnectionString);
//#if DEBUG
//        optionsBuilder.AddInterceptors(new UtcSaveChangesInterceptor());
//#endif
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.BirthDate).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("now()");
            entity.Property(e => e.DeletedAt);

            entity.HasMany(e => e.Addresses)
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.PersonId);

            entity.HasMany(e => e.Contacts)
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.PersonId);

            entity.HasMany(e => e.Documents)
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.PersonId);
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Value).IsRequired();
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("now()");
            entity.Property(e => e.DeletedAt);

            entity.HasOne(e => e.Person)
                .WithMany(e => e.Addresses)
                .HasForeignKey(e => e.PersonId);
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Value).IsRequired();
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("now()");
            entity.Property(e => e.DeletedAt);

            entity.HasOne(e => e.Person)
                .WithMany(e => e.Contacts)
                .HasForeignKey(e => e.PersonId);
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Value).IsRequired();
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("now()");
            entity.Property(e => e.DeletedAt);

            entity.HasOne(e => e.Person)
                .WithMany(e => e.Documents)
                .HasForeignKey(e => e.PersonId);
        });
    }
}
