using Domain.Models;
using Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelsConfigurations;

public class ExpenseMultiplierConfiguration : IEntityTypeConfiguration<ExpenseMultiplier>
{
    public void Configure(EntityTypeBuilder<ExpenseMultiplier> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasMaxLength(ModelsConstants.NameMaxLength);
        builder.HasOne(e => e.Expense)
            .WithMany()
            .HasForeignKey(e => e.ExpenseId);
        builder.Property(e => e.Multiplier)
            .HasPrecision(ModelsConstants.MultiplierPrecision, ModelsConstants.MultiplierScale);
    }
}