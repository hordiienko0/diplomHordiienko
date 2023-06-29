using Ctor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ctor.Infrastructure.Persistence.Configurations;
public class BuildingConfiguration : IEntityTypeConfiguration<Building>
{
    public void Configure(EntityTypeBuilder<Building> builder)
    {
        builder.Property(x => x.Id)
            .UseIdentityByDefaultColumn()
            .HasIdentityOptions(startValue: 100);

        builder.ToTable("Building").HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.HasMany(x => x.BuildingBlocks)
               .WithOne(x => x.Building)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
