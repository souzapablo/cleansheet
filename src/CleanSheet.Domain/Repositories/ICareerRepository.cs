using CleanSheet.Domain.Entities;

namespace CleanSheet.Domain.Repositories;
public interface ICareerRepository
{
    Task AddAsync(Career newCareer, CancellationToken cancellationToken = default);
     Task<List<Career>> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default); 
}
