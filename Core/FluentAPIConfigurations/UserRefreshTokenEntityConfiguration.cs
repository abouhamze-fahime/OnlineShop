using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.FluentAPIConfigurations;

public class UserRefreshTokenEntityConfiguration:IEntityTypeConfiguration<UserRefreshToken>
{
    public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        builder.ToTable("UserRefreshTokens");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.RefreshToken)
            .IsRequired();
        builder.Property(x => x.UserId)
            .IsRequired();
        builder.Property(x=>x.RefreshTokenTimeout)
             .IsRequired();

    }

}
