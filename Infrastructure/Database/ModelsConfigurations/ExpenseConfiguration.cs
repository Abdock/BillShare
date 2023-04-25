using Domain.Models;
using Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelsConfigurations;

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Amount).HasPrecision(ModelsConstants.AmountPrecision, ModelsConstants.AmountScale);
        builder.Property(e => e.Name).HasMaxLength(ModelsConstants.NameMaxLength);
        builder.HasOne(e => e.Account)
            .WithMany(e => e.Expenses)
            .HasForeignKey(e => e.AccountId);
        builder.HasOne(e => e.Creator)
            .WithMany(e => e.CreatedExpenses)
            .HasForeignKey(e => e.CreatorId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(e => e.Category)
            .WithMany()
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(e => e.ExpenseType)
            .WithMany()
            .HasForeignKey(e => e.ExpenseTypeId);
        builder.HasOne(e => e.Status)
            .WithMany()
            .HasForeignKey(e => e.StatusId);
    }
}