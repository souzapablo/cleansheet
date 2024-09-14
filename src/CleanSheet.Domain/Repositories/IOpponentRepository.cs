using CleanSheet.Domain.Entities;

namespace CleanSheet.Domain.Repositories;
public interface IOpponentRepository
{
    Task<Opponent?> GetByIdAsync(long id, CancellationToken cancellationToken);
}
