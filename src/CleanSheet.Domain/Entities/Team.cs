using CleanSheet.Domain.Abstractions;

namespace CleanSheet.Domain.Entities;
public class Team(
    string name,
    string staidum) : Entity
{
    private readonly List<Player> _squad = [];

    public string Name { get; private set; } = name;
    public string Stadium { get; private set; } = staidum;
    public IReadOnlyCollection<Player> Squad => _squad;
    public long CareerId { get; private set; }
    public Career Career { get; private set; } = null!;
}
