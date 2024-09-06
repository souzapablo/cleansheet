using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanSheet.Infrastructure.Repositories;
public class CareerRepository(
    CleanSheetDbContext context) : ICareerRepository
{
    public async Task AddAsync(Career newCareer, CancellationToken cancellationToken)
    {
        context.Careers.Add(newCareer);
        await context.SaveChangesAsync(cancellationToken);  
    }

    public async Task<List<Career>> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default) =>
        await context.Careers
            .Where(x => x.UserId == userId)
            .Include(x => x.Teams)
            .OrderByDescending(x => x.LastUpdate)
            .ToListAsync(cancellationToken);
}
