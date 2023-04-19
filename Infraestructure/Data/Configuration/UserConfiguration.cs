using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.Property(p => p.Id)
                .IsRequired();

        builder.Property(p => p.Names)
                .IsRequired()
                .HasMaxLength(200);

        builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(200);

        builder.Property(p => p.MotherLastName)
                .IsRequired()
                .HasMaxLength(200);

        builder.Property(p => p.Username)
                .IsRequired()
                .HasMaxLength(200);

        builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(200);

        builder.HasMany(p => p.Roles)
            .WithMany(p => p.Users)
            .UsingEntity<UserRols>(
                j => j.HasOne(pt => pt.Rol)
                        .WithMany(t => t.UserRols)
                        .HasForeignKey(pt => pt.RolId),
                j => j.HasOne(pt => pt.User)
                        .WithMany(p => p.UserRols)
                        .HasForeignKey(pt => pt.UserId),
                j =>
                {
                    j.HasKey(t => new { t.UserId, t.RolId });
                });

        //builder.HasMany(p => p.RefreshTokens)
        //    .WithOne(p => p.User)
        //    .HasForeignKey(p => p.UserId);
    }
}
