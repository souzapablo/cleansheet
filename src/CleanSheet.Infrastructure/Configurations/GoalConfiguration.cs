using CleanSheet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanSheet.Infrastructure.Configurations;
public class GoalConfiguration : IEntityTypeConfiguration<Goal>
{
    public void Configure(EntityTypeBuilder<Goal> builder)
    {
        builder.HasOne(g => g.PlayerScored)
            .WithMany()
            .HasForeignKey(g => g.PlayerScoredId)
            .IsRequired(false);

        builder.HasOne(g => g.Assist)
            .WithOne(a => a.Goal)
            .HasForeignKey<Assist>(a => a.Id)
            .IsRequired(false);
    }
}
