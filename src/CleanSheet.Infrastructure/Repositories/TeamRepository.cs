using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanSheet.Infrastructure.Repositories;
public class TeamRepository(
    CleanSheetDbContext context) : ITeamRepository
{
    public async Task<Team?> GetByIdAsync(long id, CancellationToken cancellationToken) =>
        await context.Teams
            .SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
}
