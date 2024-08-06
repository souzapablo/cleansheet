using CleanSheet.Domain.Abstractions;

namespace CleanSheet.Domain.Entities;
public class User(
    string username,
    string password) : Entity
{
    private readonly List<Career> _careers = [];

    public string Username { get; private set; } = username;
    public string Password { get; private set; } = password;
    public IReadOnlyCollection<Career> Careers => _careers;
}
