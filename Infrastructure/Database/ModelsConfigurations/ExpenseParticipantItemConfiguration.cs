using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelsConfigurations;

public class ExpenseParticipantItemConfiguration : IEntityTypeConfiguration<ExpenseParticipantItem>
{
    public void Configure(EntityTypeBuilder<ExpenseParticipantItem> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne(e => e.Item)
            .WithMany(e => e.ExpenseParticipantItems)
            .HasForeignKey(e => e.ItemId);
        builder.HasOne(e => e.Participant)
            .WithMany(e => e.ExpenseParticipantItems)
            .HasForeignKey(e => e.ExpenseParticipantId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(e => e.Status)
            .WithMany()
            .HasForeignKey(e => e.StatusId);
    }
}