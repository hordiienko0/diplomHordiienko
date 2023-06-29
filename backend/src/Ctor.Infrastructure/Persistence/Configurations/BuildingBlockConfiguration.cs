using Ctor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ctor.Infrastructure.Persistence.Configurations;
internal class BuildingBlockConfiguration : IEntityTypeConfiguration<BuildingBlock>
{
    public void Configure(EntityTypeBuilder<BuildingBlock> builder)
    {
        builder.Property(x => x.Id)
            .UseIdentityByDefaultColumn()
            .HasIdentityOptions(startValue: 100);

        builder.ToTable("BuildingBlock").HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.BuildingBlockName).IsRequired();
        builder.Property(x => x.Details).IsRequired();
    }
}
