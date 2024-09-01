using CleanSheet.Domain.Entities;

namespace CleanSheet.Domain.Repositories;
public interface IInitialTeamRepository
{
    Task<InitialTeam?> GetBySlugAsync(string name, CancellationToken cancellationToken = default);
}
