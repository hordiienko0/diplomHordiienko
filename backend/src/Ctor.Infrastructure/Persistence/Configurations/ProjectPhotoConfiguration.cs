using Ctor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ctor.Infrastructure.Persistence.Configurations;
public class ProjectPhotoConfiguration : IEntityTypeConfiguration<ProjectPhoto>
{
    public void Configure(EntityTypeBuilder<ProjectPhoto> builder)
    {
        builder.Property(x => x.Id)
            .UseIdentityByDefaultColumn()
            .HasIdentityOptions(startValue: 100);
        builder.ToTable("ProjectPhoto")
            .HasIndex(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.Link).IsRequired();
        builder.Property(x => x.Path).IsRequired();
        builder.Property(x => x.Type).IsRequired();

    }
}
