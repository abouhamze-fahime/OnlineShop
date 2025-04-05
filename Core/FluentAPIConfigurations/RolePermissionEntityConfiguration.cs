using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.FluentAPIConfigurations;

public class RolePermissionEntityConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermissions");

        
        builder.HasKey(rap => rap.Id);

        // Relationships

        builder.HasOne(rap => rap.Role)
               .WithMany(r => r.RolePermissions)
               .HasForeignKey(rap => rap.RoleId)
               .OnDelete(DeleteBehavior.Cascade);

        // Relationship with ApplicationPermission (many-to-one)
        builder.HasOne(rap => rap.Permission)
               .WithMany(ap => ap.RolePermissions)
               .HasForeignKey(rap => rap.PermissionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
