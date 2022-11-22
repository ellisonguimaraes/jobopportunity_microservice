using JobOpportunityMicroservice.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobOpportunityMicroservice.Infra.Data.Contexts.Configuration;

/// <summary>
/// Configuration Entity (JobCategory) to EntityFramework
/// </summary>
public class JobCategoryConfiguration : BaseEntityConfiguration<JobCategory>
{
    #region Constants
    private const string TABLE_NAME = "CategoryJob";
    private const string JOB_ADVERTISEMENT_ID_COLUMN_NAME = "job_advertisement_id";
    private const int JOB_ADVERTISEMENT_ID_MAX_LENGTH = 36;
    private const string CATEGORY_ID_COLUMN_NAME = "category_id";
    private const int CATEGORY_ID_MAX_LENGTH = 36;
    #endregion

    public override void Configure(EntityTypeBuilder<JobCategory> builder)
    {
        builder.ToTable(TABLE_NAME);
        
        builder.Property(jc => jc.JobAdvertisementId).HasColumnName(JOB_ADVERTISEMENT_ID_COLUMN_NAME).HasMaxLength(JOB_ADVERTISEMENT_ID_MAX_LENGTH).IsRequired();
        builder.Property(jc => jc.CategoryId).HasColumnName(CATEGORY_ID_COLUMN_NAME).HasMaxLength(CATEGORY_ID_MAX_LENGTH).IsRequired();
        
        builder.HasOne(jc => jc.JobAdvertisement)
            .WithMany(ja => ja.JobCategories)
            .HasForeignKey(jc => jc.JobAdvertisementId);
        
        builder.HasOne(jc => jc.Category)
            .WithMany(c => c.JobCategories)
            .HasForeignKey(jc => jc.CategoryId);
        
        base.Configure(builder);
    }
}
