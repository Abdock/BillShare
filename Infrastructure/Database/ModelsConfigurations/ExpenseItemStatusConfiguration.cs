using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelsConfigurations;

public class ExpenseItemStatusConfiguration : IEntityTypeConfiguration<ExpenseItemStatus>
{
    public void Configure(EntityTypeBuilder<ExpenseItemStatus> builder)
    {
        builder.HasKey(e => e.Id);
        var possibleStatuses = Enum.GetValues<ExpenseItemStatusId>()
            .Select(status => new ExpenseItemStatus
            {
                Id = status,
                Name = status.ToString()
            });
        builder.HasData(possibleStatuses);
    }
}