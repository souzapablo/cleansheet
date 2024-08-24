using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Repositories;

namespace CleanSheet.Infrastructure.Repositories;
public class CareerRepository(
    CleanSheetDbContext context) : ICareerRepository
{
    public async Task AddAsync(Career newCareer, CancellationToken cancellationToken)
    {
        context.Careers.Add(newCareer);
        await context.SaveChangesAsync(cancellationToken);  
    }
}
