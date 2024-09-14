using CleanSheet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanSheet.Infrastructure.Configurations;
public class AssistConfiguration : IEntityTypeConfiguration<Assist>
{
    public void Configure(EntityTypeBuilder<Assist> builder)
    {
        builder.HasOne(g => g.PlayerAssisted)
            .WithMany()
            .HasForeignKey(g => g.PlayerAssistedId);
    }
}