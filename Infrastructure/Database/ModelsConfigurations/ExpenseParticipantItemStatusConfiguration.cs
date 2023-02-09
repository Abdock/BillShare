using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelsConfigurations;

public class ExpenseParticipantItemStatusConfiguration : IEntityTypeConfiguration<ExpenseParticipantItemStatus>
{
    public void Configure(EntityTypeBuilder<ExpenseParticipantItemStatus> builder)
    {
        builder.HasKey(e => e.Id);
        var possibleStatuses = Enum.GetValues<ExpenseParticipantItemStatusId>()
            .Select(status => new ExpenseParticipantItemStatus
            {
                Id = status,
                Name = status.ToString()
            });
        builder.HasData(possibleStatuses);
    }
}