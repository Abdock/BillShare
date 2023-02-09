using Domain.Models;
using Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelsConfigurations;

public class IconConfiguration : IEntityTypeConfiguration<Icon>
{
    public void Configure(EntityTypeBuilder<Icon> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Url).HasMaxLength(ModelsConstants.UrlMaxLength);
        builder.HasOne(e => e.ExpenseCategory)
            .WithOne(e => e.Icon)
            .HasForeignKey<Icon>(e => e.ExpenseCategoryId);
    }
}