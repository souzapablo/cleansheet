using CleanSheet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanSheet.Infrastructure.Configurations;
internal class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.Property(x => x.Birthday)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.KitNumber)
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(x => x.Overall)
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(x => x.Position)
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnType("varchar(80)")
            .IsRequired();
    }
}
