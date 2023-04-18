using Domain.Models;
using Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelsConfigurations;

public class GroupsConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.GroupName).HasMaxLength(ModelsConstants.NameMaxLength);
        builder.HasMany(e => e.Participants)
            .WithMany(e => e.Groups);
        builder.HasOne(e => e.Creator)
            .WithMany(e => e.CreatedGroups)
            .HasForeignKey(e => e.CreatorId);
    }
}