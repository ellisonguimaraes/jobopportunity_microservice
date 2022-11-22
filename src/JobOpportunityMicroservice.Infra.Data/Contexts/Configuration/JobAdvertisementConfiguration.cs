using JobOpportunityMicroservice.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobOpportunityMicroservice.Infra.Data.Contexts.Configuration;

/// <summary>
/// Configuration Entity (JobAdvertisement) to EntityFramework
/// </summary>
public class JobAdvertisementConfiguration : BaseEntityConfiguration<JobAdvertisement>
{
    #region Constants
    private const string TABLE_NAME = "JobAdvertisement";
    private const string TITLE_COLUMN_NAME = "title";
    private const int TITLE_MAX_LENGTH = 150;
    private const string COMPANY_COLUMN_NAME = "company";
    private const int COMPANY_MAX_LENGTH = 80;
    private const string DESCRIPTION_COLUMN_NAME = "description";
    private const string MODALITY_COLUMN_NAME = "modality";
    private const string BENEFIT_COLUMN_NAME = "benefit";
    private const string MIN_PAY_RANGE_COLUMN_NAME = "min_payrange";
    private const string MAX_PAY_RANGE_COLUMN_NAME = "max_payrange";
    private const string REQUEREMENTS_COLUMN_NAME = "requerements";
    private const string MONTHLY_HOURS_COLUMN_NAME = "monthly_hours";
    private const string EMAIL_COLUMN_NAME = "email";
    private const int EMAIL_MAX_LENGTH = 100;
    private const string PHONE_NUMBER_COLUMN_NAME = "phone_number";
    private const int PHONE_NUMBER_MAX_LENGTH = 45;
    private const string LINK_COLUMN_NAME = "link";
    private const int LINK_MAX_LENGTH = 300;
    private const string DATE_LIMIT_COLUMN_NAME = "date_limit";
    private const string IS_ACTIVE_COLUMN_NAME = "is_active";
    private const string USER_ID_COLUMN_NAME = "user_id";
    private const int USER_ID_MAX_LENGTH = 36;
    #endregion

    public override void Configure(EntityTypeBuilder<JobAdvertisement> builder)
    {
        builder.ToTable(TABLE_NAME);

        builder.Property(ja => ja.Title).HasColumnName(TITLE_COLUMN_NAME).HasMaxLength(TITLE_MAX_LENGTH).IsRequired();
        builder.Property(ja => ja.Company).HasColumnName(COMPANY_COLUMN_NAME).HasMaxLength(COMPANY_MAX_LENGTH).IsRequired();
        builder.Property(ja => ja.Description).HasColumnName(DESCRIPTION_COLUMN_NAME).IsRequired();
        builder.Property(ja => ja.Modality).HasColumnName(MODALITY_COLUMN_NAME).IsRequired();
        builder.Property(ja => ja.Benefit).HasColumnName(BENEFIT_COLUMN_NAME);
        builder.Property(ja => ja.MinPayRange).HasColumnName(MIN_PAY_RANGE_COLUMN_NAME);
        builder.Property(ja => ja.MaxPayRange).HasColumnName(MAX_PAY_RANGE_COLUMN_NAME);
        builder.Property(ja => ja.Requerements).HasColumnName(REQUEREMENTS_COLUMN_NAME).IsRequired();
        builder.Property(ja => ja.MonthlyHours).HasColumnName(MONTHLY_HOURS_COLUMN_NAME).IsRequired();
        builder.Property(ja => ja.Email).HasColumnName(EMAIL_COLUMN_NAME).HasMaxLength(EMAIL_MAX_LENGTH).IsRequired();
        builder.Property(ja => ja.PhoneNumber).HasColumnName(PHONE_NUMBER_COLUMN_NAME).HasMaxLength(PHONE_NUMBER_MAX_LENGTH).IsRequired();
        builder.Property(ja => ja.Link).HasColumnName(LINK_COLUMN_NAME).HasMaxLength(LINK_MAX_LENGTH);
        builder.Property(ja => ja.DateLimit).HasColumnName(DATE_LIMIT_COLUMN_NAME).IsRequired();
        builder.Property(ja => ja.IsActive).HasColumnName(IS_ACTIVE_COLUMN_NAME).IsRequired();
        builder.Property(ja => ja.UserId).HasColumnName(USER_ID_COLUMN_NAME).HasMaxLength(USER_ID_MAX_LENGTH).IsRequired();

        builder.HasMany(ja => ja.JobCategories)
            .WithOne(jc => jc.JobAdvertisement)
            .HasForeignKey(jc => jc.JobAdvertisementId);
        
        base.Configure(builder);
    }
}
