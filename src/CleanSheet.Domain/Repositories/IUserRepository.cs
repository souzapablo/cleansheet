using CleanSheet.Domain.Entities;

namespace CleanSheet.Domain.Repositories;
public interface IUserRepository
{
    Task AddAsync(User newUser, CancellationToken cancellationToken);
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
}
