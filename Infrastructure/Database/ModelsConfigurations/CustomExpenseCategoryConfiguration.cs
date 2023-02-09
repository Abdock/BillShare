using Domain.Models;
using Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelsConfigurations;

public class CustomExpenseCategoryConfiguration : IEntityTypeConfiguration<CustomExpenseCategory>
{
    public void Configure(EntityTypeBuilder<CustomExpenseCategory> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne(e => e.Icon)
            .WithOne(e => e.ExpenseCategory)
            .HasForeignKey<Icon>(e => e.ExpenseCategoryId);
        builder.Property(e => e.Name).HasMaxLength(ModelsConstants.NameMaxLength);
    }
}