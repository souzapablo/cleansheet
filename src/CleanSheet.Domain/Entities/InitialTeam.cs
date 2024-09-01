using CleanSheet.Domain.Abstractions;

namespace CleanSheet.Domain.Entities;

public class InitialTeam : Entity
{
    private readonly List<InitialPlayer> _initialSquad = [];

    protected InitialTeam() { }
    public InitialTeam(string slug, string name, string stadium, List<InitialPlayer> initialSquad)
    {
        Slug = slug;
        Name = name;
        Stadium = stadium;
        _initialSquad = initialSquad;
    }

    public string Slug { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Stadium { get; private set; } = string.Empty;
    public IReadOnlyCollection<InitialPlayer> InitialSquad => _initialSquad;
}
