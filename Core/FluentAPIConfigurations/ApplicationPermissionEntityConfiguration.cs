using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.FluentAPIConfigurations;

public class ApplicationPermissionEntityConfiguration : IEntityTypeConfiguration<ApplicationEntityConfiguration>
{
    public void Configure(EntityTypeBuilder<ApplicationEntityConfiguration> builder)
    {
        throw new NotImplementedException();
    }
}
