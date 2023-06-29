using Ctor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ctor.Infrastructure.Persistence.Configurations;

public class CompanyLogoConfiguration: IEntityTypeConfiguration<CompanyLogo>
{
    public void Configure(EntityTypeBuilder<CompanyLogo> builder)
    {
        builder.Property(x => x.Id)
            .UseIdentityByDefaultColumn()
            .HasIdentityOptions(startValue: 100);

        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.Link).IsRequired();
        builder.Property(x => x.Path).IsRequired();
        builder.Property(x => x.Type).IsRequired();
    }
}