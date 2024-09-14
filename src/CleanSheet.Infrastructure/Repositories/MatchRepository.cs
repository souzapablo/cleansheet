using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Repositories;

namespace CleanSheet.Infrastructure.Repositories;
public class MatchRepository(
    CleanSheetDbContext context) : IMatchRepository
{
    public async Task AddAsync(Match match, CancellationToken cancellationToken)
    {
        context.Add(match);
        await context.SaveChangesAsync(cancellationToken);
    }
}
