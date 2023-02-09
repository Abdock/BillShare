using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelsConfigurations;

public class ExpenseParticipantStatusConfiguration : IEntityTypeConfiguration<ExpenseParticipantStatus>
{
    public void Configure(EntityTypeBuilder<ExpenseParticipantStatus> builder)
    {
        builder.HasKey(e => e.Id);
        var possibleStatuses = Enum.GetValues<ExpenseParticipantStatusId>()
            .Select(status => new ExpenseParticipantStatus
            {
                Id = status,
                Name = status.ToString()
            });
        builder.HasData(possibleStatuses);
    }
}