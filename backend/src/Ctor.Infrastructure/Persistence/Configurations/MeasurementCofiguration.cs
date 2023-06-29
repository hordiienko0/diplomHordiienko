using Ctor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ctor.Infrastructure.Persistence.Configurations;
public class MeasurementCofiguration : IEntityTypeConfiguration<Measurement>
{
    public void Configure(EntityTypeBuilder<Measurement> builder)
    {
        builder.Property(x => x.Id)
       .UseIdentityByDefaultColumn()
       .HasIdentityOptions(startValue: 100);

        builder.ToTable("Measurement").HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.Name).IsRequired();
        builder.HasMany(x => x.Materials)
            .WithOne(x => x.Measurement);
    }
}