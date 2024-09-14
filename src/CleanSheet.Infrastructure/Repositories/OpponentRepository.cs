using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanSheet.Infrastructure.Repositories;
public class OpponentRepository(
    CleanSheetDbContext context) : IOpponentRepository
{
    public async Task<Opponent?> GetByIdAsync(long id, CancellationToken cancellationToken) =>
        await context.Opponents
            .SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
}
