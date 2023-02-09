using Domain.Models;
using Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelsConfigurations;

public class PasswordConfiguration : IEntityTypeConfiguration<Password>
{
    public void Configure(EntityTypeBuilder<Password> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne(e => e.Customer)
            .WithOne(e => e.Password)
            .HasForeignKey<Customer>(e => e.PasswordId);
        builder.Property(e => e.EncryptedPassword).HasMaxLength(ModelsConstants.EncryptedPasswordMaxLength);
        builder.Property(e => e.Salt).HasMaxLength(ModelsConstants.SaltMaxLength);
    }
}