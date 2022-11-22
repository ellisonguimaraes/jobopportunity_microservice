using JobOpportunityMicroservice.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobOpportunityMicroservice.Infra.Data.Contexts.Configuration;

/// <summary>
/// Configuration Entity (Address) to EntityFramework
/// </summary>
public class AddressConfiguration : BaseEntityConfiguration<Address>
{
    #region Constants
    private const string TABLE_NAME = "Address";
    private const string STREET_COLUMN_NAME = "street";
    private const int STREET_MAX_LENGTH = 45;
    private const string DISTRICT_COLUMN_NAME = "district";
    private const int DISTRICT_MAX_LENGTH = 45;
    private const string CITY_COLUMN_NAME = "city";
    private const int CITY_MAX_LENGTH = 45;
    private const string STATE_COLUMN_NAME = "state";
    private const int STATE_MAX_LENGTH = 45;
    private const string COUNTRY_COLUMN_NAME = "country";
    private const int COUNTRY_MAX_LENGTH = 45;
    private const string JOB_ADVERTISEMENT_ID_COLUMN_NAME = "job_advertisement_id";
    private const int JOB_ADVERTISEMENT_ID_MAX_LENGTH = 36;
    #endregion

    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable(TABLE_NAME);
        
        builder.Property(a => a.Street).HasColumnName(STREET_COLUMN_NAME).HasMaxLength(STREET_MAX_LENGTH);
        builder.Property(a => a.District).HasColumnName(DISTRICT_COLUMN_NAME).HasMaxLength(DISTRICT_MAX_LENGTH);
        builder.Property(a => a.City).HasColumnName(CITY_COLUMN_NAME).HasMaxLength(CITY_MAX_LENGTH).IsRequired();
        builder.Property(a => a.State).HasColumnName(STATE_COLUMN_NAME).HasMaxLength(STATE_MAX_LENGTH).IsRequired();
        builder.Property(a => a.Country).HasColumnName(COUNTRY_COLUMN_NAME).HasMaxLength(COUNTRY_MAX_LENGTH).IsRequired();
        builder.Property(a => a.JobAdvertisementId).HasColumnName(JOB_ADVERTISEMENT_ID_COLUMN_NAME).HasMaxLength(JOB_ADVERTISEMENT_ID_MAX_LENGTH).IsRequired();
        
        builder.HasIndex(a => a.JobAdvertisementId).IsUnique();

        builder.HasOne(a => a.JobAdvertisement)
            .WithOne(j => j.Address)
            .HasForeignKey<Address>(a => a.JobAdvertisementId);
        
        base.Configure(builder);
    }
}
