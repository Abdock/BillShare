using Domain.Models;
using Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelsConfigurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Ignore(e => e.Friends);
        builder.Ignore(e => e.Friendships);
        builder.Property(e => e.Name).HasMaxLength(ModelsConstants.NameMaxLength);
        builder.Property(e => e.Email).HasMaxLength(ModelsConstants.EmailMaxLength);
        builder.Property(e => e.ExternalId).HasMaxLength(ModelsConstants.ExternalIdMaxLength);
        builder.Property(e => e.TelegramHandle).HasMaxLength(ModelsConstants.TelegramHandleMaxLength);
        builder.Property(e => e.AvatarUrl).HasMaxLength(ModelsConstants.UrlMaxLength);
        builder.Property(e => e.PhoneNumber).HasMaxLength(ModelsConstants.PhoneMaxLength);
        builder.HasOne(e => e.Password)
            .WithOne(e => e.Customer)
            .HasForeignKey<Password>(e => e.CustomerId);
        builder.HasMany(e => e.ExpenseCategories)
            .WithOne(e => e.Customer)
            .HasForeignKey(e => e.CustomerId);
        builder.HasOne(e => e.Role)
            .WithMany()
            .HasForeignKey(e => e.RoleId);
        builder.HasMany(e => e.CreatedGroups)
            .WithOne(e => e.Creator)
            .HasForeignKey(e => e.CreatorId);
    }
}