using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelsConfigurations;

public class ExpenseStatusConfiguration : IEntityTypeConfiguration<ExpenseStatus>
{
    public void Configure(EntityTypeBuilder<ExpenseStatus> builder)
    {
        builder.HasKey(e => e.Id);
        var possibleStatuses = Enum.GetValues<ExpenseStatusId>()
            .Select(status => new ExpenseStatus
            {
                Id = status,
                Name = status.ToString()
            });
        builder.HasData(possibleStatuses);
    }
}