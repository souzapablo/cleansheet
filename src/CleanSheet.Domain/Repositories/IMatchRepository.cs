using CleanSheet.Domain.Entities;

namespace CleanSheet.Domain.Repositories;
public interface IMatchRepository
{
    Task AddAsync(Match match, CancellationToken cancellationToken);
}
