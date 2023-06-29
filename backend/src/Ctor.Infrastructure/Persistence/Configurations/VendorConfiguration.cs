using Ctor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ctor.Infrastructure.Persistence.Configurations;
public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
{
    public void Configure(EntityTypeBuilder<Vendor> builder)
    {
        builder.Property(x => x.Id)
            .UseIdentityByDefaultColumn()
            .HasIdentityOptions(startValue: 100);

        builder.ToTable("Vendor").HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.VendorName).IsRequired();
        builder.Property(x => x.Phone).IsRequired();
        builder.Property(x => x.Website).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.CompanyId).IsRequired();
        builder.HasOne(x => x.Company)
            .WithMany(x => x.Vendors)
            .HasForeignKey(x => x.CompanyId);
    }
}
