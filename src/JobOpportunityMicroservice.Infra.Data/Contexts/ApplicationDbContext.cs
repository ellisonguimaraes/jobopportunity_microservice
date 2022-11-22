using JobOpportunityMicroservice.Domain;
using JobOpportunityMicroservice.Infra.Data.Contexts.Configuration;
using Microsoft.EntityFrameworkCore;

namespace JobOpportunityMicroservice.Infra.Data.Contexts;

/// <summary>
/// Entity Framework Database Context
/// </summary>
public class ApplicationDbContext : DbContext
{
    public DbSet<Address> Addresses { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<JobAdvertisement> JobAdvertisements { get; set; }

    public DbSet<JobCategory> JobCategories { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    /// <summary>
    /// Entities configuring
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new AddressConfiguration().Configure(modelBuilder.Entity<Address>());
        new CategoryConfiguration().Configure(modelBuilder.Entity<Category>());
        new JobAdvertisementConfiguration().Configure(modelBuilder.Entity<JobAdvertisement>());
        new JobCategoryConfiguration().Configure(modelBuilder.Entity<JobCategory>());

        base.OnModelCreating(modelBuilder);
    }
}
