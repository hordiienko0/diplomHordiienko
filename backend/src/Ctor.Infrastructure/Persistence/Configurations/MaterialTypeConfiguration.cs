using Ctor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ctor.Infrastructure.Persistence.Configurations;
public class MaterialTypeConfiguration : IEntityTypeConfiguration<MaterialType>
{
    public void Configure(EntityTypeBuilder<MaterialType> builder)
    {
        builder.Property(x => x.Id)
        .UseIdentityByDefaultColumn()
        .HasIdentityOptions(startValue: 100);

        builder.ToTable("MaterialType").HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.Name).IsRequired();
        builder.HasMany(x => x.Materials)
            .WithOne(x => x.MaterialType);
    }
}