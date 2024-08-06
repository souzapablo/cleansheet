using CleanSheet.Domain.Abstractions;

namespace CleanSheet.Domain.Entities;
public class Career(
    string manager) : Entity
{
    public string Manager { get; private set; } = manager;
    public DateTime LastUpdate { get; private set; } = DateTime.UtcNow;
    public long UserId { get; private set; }
    public User User { get; private set; } = null!;
}
