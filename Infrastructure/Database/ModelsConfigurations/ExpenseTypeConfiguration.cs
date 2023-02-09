using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelsConfigurations;

public class ExpenseTypeConfiguration : IEntityTypeConfiguration<ExpenseType>
{
    public void Configure(EntityTypeBuilder<ExpenseType> builder)
    {
        var possibleStatuses = Enum.GetValues<ExpenseTypeId>()
            .Select(type => new ExpenseType
            {
                Id = type,
                Name = type.ToString()
            });
        builder.HasKey(e => e.Id);
        builder.HasData(possibleStatuses);
    }
}