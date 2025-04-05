using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.FluentAPIConfigurations;

public class ApplicationEntityConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.ToTable("Applications");
        builder.HasKey(a => a.Id);
        builder.Property(e => e.ApplicationName)
            .IsRequired()
            .HasColumnName("Title")
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250);



       // builder.HasMany(a => a.ApplicationPermissions)
              // .WithOne(ap => ap.Application)
             //  .HasForeignKey(ap => ap.ApplicationId)
              // .OnDelete(DeleteBehavior.Cascade);
    }
}
