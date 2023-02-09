using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelsConfigurations;

public class ExpenseParticipantConfiguration : IEntityTypeConfiguration<ExpenseParticipant>
{
    public void Configure(EntityTypeBuilder<ExpenseParticipant> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne(e => e.Status)
            .WithMany()
            .HasForeignKey(e => e.StatusId);
        builder.HasOne(e => e.Expense)
            .WithMany(e => e.ExpenseParticipants)
            .HasForeignKey(e => e.ExpenseId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(e => e.Customer)
            .WithMany()
            .HasForeignKey(e => e.CustomerId);
    }
}