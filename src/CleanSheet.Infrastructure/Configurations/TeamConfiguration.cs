using CleanSheet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanSheet.Infrastructure.Configurations;
internal class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.Property(x => x.Name)
            .HasColumnType("varchar(80)")
            .IsRequired();

        builder.Property(x => x.Stadium)
            .HasColumnType("varchar(80)")
            .IsRequired();
    }
}
