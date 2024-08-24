using CleanSheet.Domain.Entities;

namespace CleanSheet.Domain.Repositories;
public interface IUserRepository
{
    Task AddAsync(User newUser, CancellationToken cancellationToken = default);
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(long userId, CancellationToken cancellationToken = default);
}
