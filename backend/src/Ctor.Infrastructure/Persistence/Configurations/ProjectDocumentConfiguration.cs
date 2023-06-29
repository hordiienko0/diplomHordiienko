using Ctor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ctor.Infrastructure.Persistence.Configurations;
public class ProjectDocumentConfiguration : IEntityTypeConfiguration<ProjectDocument>
{
    public void Configure(EntityTypeBuilder<ProjectDocument> builder)
    {
        builder.Property(x => x.Id)
            .UseIdentityByDefaultColumn()
            .HasIdentityOptions(startValue: 100);

        builder.ToTable("ProjectDocument").HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.HasOne(x => x.Building)
               .WithMany(x=>x.ProjectDocuments)
               .HasForeignKey(p=>p.BuildingId);
        builder.HasOne(x => x.Document)
               .WithOne(x => x.ProjectDocument)
               .HasForeignKey<ProjectDocument>(p => p.DocumentId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
