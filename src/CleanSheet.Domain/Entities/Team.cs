using CleanSheet.Domain.Abstractions;

namespace CleanSheet.Domain.Entities;
public class Team : Entity
{
    private readonly List<Player> _squad = []; 

    protected Team() { }
    public Team(string name,string stadium)
    {
        Name = name;
        Stadium = stadium;
    }

    public string Name { get; private set; } = string.Empty;
    public string Stadium { get; private set; } = string.Empty;
    public IReadOnlyCollection<Player> Squad => _squad;
    public long CareerId { get; private set; }
    public Career Career { get; private set; } = null!;
}
