using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelsConfigurations;

public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
{
    public void Configure(EntityTypeBuilder<Friendship> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne(e => e.Friend)
            .WithMany(e => e.FriendFriendships)
            .HasForeignKey(e => e.FriendId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(e => e.User)
            .WithMany(e => e.UserFriendships)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}