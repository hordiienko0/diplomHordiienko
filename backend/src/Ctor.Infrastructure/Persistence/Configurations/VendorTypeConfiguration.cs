using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ctor.Infrastructure.Persistence.Configurations;
public class VendorTypeConfiguration : IEntityTypeConfiguration<VendorType>
{
    public void Configure(EntityTypeBuilder<VendorType> builder)
    {
        builder.Property(x => x.Id)
           .UseIdentityByDefaultColumn()
           .HasIdentityOptions(startValue: 100);

        builder.ToTable("VendorType").HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.Name).IsRequired();
       
    }
}
