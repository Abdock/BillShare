using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelsConfigurations;

public class FriendshipStatusConfiguration : IEntityTypeConfiguration<FriendshipStatus>
{
    public void Configure(EntityTypeBuilder<FriendshipStatus> builder)
    {
        builder.Property(e=>e.Id)
            .HasConversion<int>();
        var possibleStatuses = Enum.GetValues<FriendshipStatusId>()
            .Select(id => new FriendshipStatus
            {
                Id = id,
                Name = id.ToString()
            });
        builder.HasData(possibleStatuses);
    }
}