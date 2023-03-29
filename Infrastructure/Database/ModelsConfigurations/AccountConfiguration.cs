using Domain.Models;
using Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelsConfigurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasMaxLength(ModelsConstants.NameMaxLength);
        builder.Property(e => e.ExternalId).HasMaxLength(ModelsConstants.ExternalIdMaxLength);
        builder.Property(e => e.Amount).HasPrecision(ModelsConstants.AmountPrecision, ModelsConstants.AmountScale);
        builder.HasOne(e => e.Customer)
            .WithMany(e => e.Accounts)
            .HasForeignKey(e => e.UserId);
        builder.HasOne(e => e.Status)
            .WithMany()
            .HasForeignKey(e=>e.StatusId);
    }
}