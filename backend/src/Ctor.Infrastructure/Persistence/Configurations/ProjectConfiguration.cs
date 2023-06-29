using Ctor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ctor.Infrastructure.Persistence.Configurations;
public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Property(x => x.Id)
            .UseIdentityByDefaultColumn()
            .HasIdentityOptions(startValue: 100);

        builder.ToTable("Project").HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.HasIndex(x => x.ProjectId).IsUnique();
        builder.Property(x => x.ProjectName).IsRequired();
        builder.Property(x => x.ProjectName).IsRequired();
        builder.Property(x => x.ProjectType).IsRequired();
        builder.Property(x => x.Country).IsRequired();
        builder.Property(x => x.City).IsRequired();
        builder.Property(x => x.Address).IsRequired();
        builder.Property(x => x.Budget).IsRequired();
        builder.Property(x => x.StartTime).IsRequired();
        builder.Property(x => x.EndTime).IsRequired();
        builder.HasMany(x => x.Building)
                .WithOne(x => x.Project)
                .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.Phases)
               .WithOne(x => x.Project)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.ProjectPhotos)
            .WithOne(x => x.Project)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.ProjectDocuments)
            .WithOne(x => x.Project)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
