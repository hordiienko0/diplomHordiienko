using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ctor.Infrastructure.Persistence.Configurations;
public class RequiredMaterialConfiguration : IEntityTypeConfiguration<RequiredMaterial>
{
    public void Configure(EntityTypeBuilder<RequiredMaterial> builder)
    {
        builder.Property(x => x.Id)
            .UseIdentityByDefaultColumn()
            .HasIdentityOptions(startValue: 100);
        builder.ToTable("RequiredMaterials")
            .HasIndex(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.MaterialId).IsRequired();
        builder.Property(x => x.BuildingId).IsRequired();
        builder.Property(x => x.Amount).IsRequired();

    }
}
