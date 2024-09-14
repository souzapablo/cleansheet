using CleanSheet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanSheet.Infrastructure.Configurations;
public class OpponentConfiguration : IEntityTypeConfiguration<Opponent>
{
    public void Configure(EntityTypeBuilder<Opponent> builder)
    {
        builder.Property(x => x.Name)
            .HasColumnType("varchar(80)")
            .IsRequired();

        builder.Property(x => x.Stadium)
            .HasColumnType("varchar(80)")
            .IsRequired();
    }
}
