using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.FluentAPIConfigurations;

public class PermissionEntityConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");
        builder.HasKey(x => x.Id);
        builder.Property(ap => ap.Title)
                .IsRequired()
                .HasColumnType("nvarchar(150)")
                .HasMaxLength(150); 

        // Relationship with RolePermissions (1-to-many)
        builder.HasMany(ap => ap.RolePermissions)
               .WithOne(rap => rap.Permission)
               .HasForeignKey(rap => rap.PermissionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
