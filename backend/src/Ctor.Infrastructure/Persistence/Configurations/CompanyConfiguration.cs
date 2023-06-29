using Ctor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ctor.Infrastructure.Persistence.Configurations;
public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.Property(x => x.Id)
            .UseIdentityByDefaultColumn()
            .HasIdentityOptions(startValue: 100);

        builder.ToTable("Company").HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.CompanyId).IsRequired();
        builder.Property(x => x.CompanyName).IsRequired();
        builder.Property(x => x.Country).IsRequired();
        builder.Property(x => x.City).IsRequired();
        builder.Property(x => x.Address).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.JoinDate).IsRequired();
        builder.HasMany(x => x.Materials)
            .WithOne(x => x.Company)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.Users)
            .WithOne(x => x.Company)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.Projects)
            .WithOne(x => x.Company)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.CompanyLogo)
            .WithOne(x => x.Company)
            .HasForeignKey<CompanyLogo>(x=>x.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.Vendors)
            .WithOne(x => x.Company);
            
    }
}
