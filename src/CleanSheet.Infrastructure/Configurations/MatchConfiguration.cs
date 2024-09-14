using CleanSheet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanSheet.Infrastructure.Configurations;

public class MatchConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.Property(x => x.Date)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.Location)
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(x => x.Result)
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(x => x.Competition)
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(x => x.HomeGoals)
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(x => x.AwayGoals)
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(x => x.Stadium)
            .HasColumnType("varchar(80)")
            .IsRequired();
    }
}
