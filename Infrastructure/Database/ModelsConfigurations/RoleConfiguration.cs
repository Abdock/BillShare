using Domain.Enums;
using Domain.Models;
using Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelsConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasMaxLength(ModelsConstants.NameMaxLength);
        var possibleValues = Enum.GetValues<RoleId>()
            .Select(id => new Role
            {
                Id = id,
                Name = id.ToString()
            });
        builder.HasData(possibleValues);
    }
}