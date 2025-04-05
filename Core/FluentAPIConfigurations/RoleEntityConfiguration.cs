using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.FluentAPIConfigurations;

public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        builder.HasKey(x => x.Id);
        builder.Property(e => e.RoleName)
            .IsRequired()
            .HasColumnType("nvarchar(250)")
            .HasColumnName("Title")
            .HasMaxLength(250);

        builder.HasMany(r => r.RolePermissions)
             .WithOne(rap => rap.Role)
             .HasForeignKey(rap => rap.RoleId)
             .OnDelete(DeleteBehavior.Cascade); // or Restrict/SetNull

        // Relationship with UserRoles (1-to-many)
        builder.HasMany(r => r.UserRoles)
               .WithOne(ur => ur.Role)
               .HasForeignKey(ur => ur.RoleId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
