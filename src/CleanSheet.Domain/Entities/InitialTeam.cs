using CleanSheet.Domain.Abstractions;

namespace CleanSheet.Domain.Entities;

public class InitialTeam : Entity
{
    private readonly List<InitialPlayer> _initialSquad = [];

    public string Name { get; private set; } = string.Empty;
    public string Stadium { get; private set; } = string.Empty;
    public IReadOnlyCollection<InitialPlayer> InitialSquad => _initialSquad;
}
