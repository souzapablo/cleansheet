using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanSheet.Infrastructure.Repositories;
public class InitialTeamRepository(
    CleanSheetDbContext context) : IInitialTeamRepository
{
    public async Task<InitialTeam?> GetBySlugAsync(string name, CancellationToken cancellationToken = default) =>
        await context.InitialTeams
            .SingleOrDefaultAsync(x => x.Slug.Equals(name));
}
