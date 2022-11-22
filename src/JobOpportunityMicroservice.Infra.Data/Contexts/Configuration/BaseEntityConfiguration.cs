using JobOpportunityMicroservice.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobOpportunityMicroservice.Infra.Data.Contexts.Configuration;

public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    #region Constants
    private const string ID_COLUMN_NAME = "id";
    private const int ID_MAX_LENGTH = 36;
    private const string CREATED_AT_COLUMN_NAME = "created_at";
    private const string UPDATED_AT_COLUMN_NAME = "updated_at";
    #endregion
    
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id).HasColumnName(ID_COLUMN_NAME).HasMaxLength(ID_MAX_LENGTH).IsRequired();
        builder.Property(b => b.CreatedAt).HasColumnName(CREATED_AT_COLUMN_NAME).IsRequired();
        builder.Property(b => b.UpdateAt).HasColumnName(UPDATED_AT_COLUMN_NAME).IsRequired();
    }
}