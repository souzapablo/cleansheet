using CleanSheet.Domain.Entities;

namespace CleanSheet.Domain.Repositories;
public interface ITeamRepository
{
    Task<Team?> GetByIdAsync(long id, CancellationToken cancellationToken);
}
