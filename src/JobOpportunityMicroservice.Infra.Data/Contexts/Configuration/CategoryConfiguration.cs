using JobOpportunityMicroservice.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobOpportunityMicroservice.Infra.Data.Contexts.Configuration;

/// <summary>
/// Configuration Entity (Category) to EntityFramework
/// </summary>
public class CategoryConfiguration : BaseEntityConfiguration<Category>
{
    #region Constants
    private const string TABLE_NAME = "Category";
    private const string NAME_COLUMN_NAME = "name";
    private const int NAME_MAX_LENGTH = 45;
    #endregion

    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(TABLE_NAME);

        builder.Property(c => c.Name).HasColumnName(NAME_COLUMN_NAME).HasMaxLength(NAME_MAX_LENGTH).IsRequired();
        
        base.Configure(builder);
    }
}
