using CleanSheet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanSheet.Infrastructure.Configurations;
internal class CareerConfiguration : IEntityTypeConfiguration<Career>
{
    public void Configure(EntityTypeBuilder<Career> builder)
    {
        builder.OwnsOne(typeof(Manager), "Manager", m =>
        {
            m.Property<string>("FirstName")
                .HasColumnName("ManagerFirstName")
                .HasColumnType("varchar(12)")
                .IsRequired();

            m.Property<string>("LastName")
                .HasColumnName("ManagerLastName")
                .HasColumnType("varchar(12)")
                .IsRequired();
        });
    }
}
