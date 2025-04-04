using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.FluentAPIConfigurations;

public class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRoleEntityConfiguration>
{
    public void Configure(EntityTypeBuilder<UserRoleEntityConfiguration> builder)
    {
        throw new NotImplementedException();
    }
}
