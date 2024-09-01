using CleanSheet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanSheet.Infrastructure.Configurations;

class InitialTeamConfiguration : IEntityTypeConfiguration<InitialTeam>
{
    public void Configure(EntityTypeBuilder<InitialTeam> builder)
    {
        builder.Property(x => x.Name)
            .HasColumnType("varchar(80)")
            .IsRequired();

        builder.Property(x => x.Stadium)
            .HasColumnType("varchar(80)")
            .IsRequired();

        builder.Property(x => x.Slug)
            .HasColumnType("varchar(80)")
            .IsRequired();

        builder.HasIndex(x => x.Slug)
            .IsUnique();

        builder.OwnsMany(x => x.InitialSquad, sa =>
        {
            sa.ToJson();
            sa.Property(p => p.Name).HasColumnType("varchar(50)").IsRequired();
            sa.Property(p => p.KitNumber).IsRequired();
            sa.Property(p => p.Overall).IsRequired();
            sa.Property(p => p.Birthday).HasColumnType("date").IsRequired();
            sa.Property(p => p.Position).HasConversion<string>().IsRequired();
        });

        builder.Navigation(x => x.InitialSquad).HasField("_initialSquad");
    }
}
