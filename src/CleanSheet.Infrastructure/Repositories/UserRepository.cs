using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanSheet.Infrastructure.Repositories;
public class UserRepository(
    CleanSheetDbContext context) : IUserRepository
{
    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken) =>
        await context.Users
            .SingleOrDefaultAsync(u => u.Username == username, cancellationToken: cancellationToken);

    public async Task AddAsync(User newUser, CancellationToken cancellationToken)
    {
        await context.Users.AddAsync(newUser, cancellationToken);
        await context.SaveChangesAsync();
    }
}
