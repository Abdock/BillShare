using Domain.Models;
using Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelsConfigurations;

public class ExpenseItemConfiguration : IEntityTypeConfiguration<ExpenseItem>
{
    public void Configure(EntityTypeBuilder<ExpenseItem> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne(e => e.Expense)
            .WithMany(e => e.ExpenseItems)
            .HasForeignKey(e => e.ExpenseId);
        builder.HasOne(e => e.Status)
            .WithMany()
            .HasForeignKey(e => e.StatusId);
        builder.Property(e => e.Name).HasMaxLength(ModelsConstants.NameMaxLength);
        builder.Property(e => e.Amount).HasPrecision(ModelsConstants.AmountPrecision, ModelsConstants.AmountScale);
    }
}