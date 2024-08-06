using CleanSheet.Domain.Abstractions;

namespace CleanSheet.Domain.Entities;
public class User : Entity
{
    private readonly List<Career> _careers = [];

    protected User() { }
    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public IReadOnlyCollection<Career> Careers => _careers;
}
