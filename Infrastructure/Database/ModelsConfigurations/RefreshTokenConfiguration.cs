using Domain.Models;
using Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelsConfigurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne(e => e.Owner)
            .WithMany()
            .HasForeignKey(e => e.OwnerId);
        builder.Property(e => e.Token).HasMaxLength(ModelsConstants.TokenMaxLength);
    }
}