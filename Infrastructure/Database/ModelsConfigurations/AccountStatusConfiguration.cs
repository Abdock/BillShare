using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelsConfigurations;

public class AccountStatusConfiguration : IEntityTypeConfiguration<AccountStatus>
{
    public void Configure(EntityTypeBuilder<AccountStatus> builder)
    {
        builder.Property(e => e.Id)
            .HasConversion<int>();
        var possibleStatuses = Enum.GetValues<AccountStatusId>()
            .Select(id => new AccountStatus
            {
                Id = id,
                Name = id.ToString()
            });
        builder.HasData(possibleStatuses);
    }
}