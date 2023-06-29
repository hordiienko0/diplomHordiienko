using Ctor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ctor.Infrastructure.Persistence.Configurations;
public class ProjectNoteConfiguration : IEntityTypeConfiguration<ProjectNote>
{
    public void Configure(EntityTypeBuilder<ProjectNote> builder)
    {
        builder.Property(x => x.Id)
            .UseIdentityByDefaultColumn()
            .HasIdentityOptions(startValue: 100);

        builder.ToTable("ProjectNote").HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.Text).IsRequired();
        builder.HasOne(x => x.Project)
               .WithMany(x => x.ProjectNote)
               .HasForeignKey(x => x.ProjectId)
               .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.User)
               .WithMany(x => x.ProjectNote)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
