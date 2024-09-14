using CleanSheet.Domain.Abstractions;

namespace CleanSheet.Domain.Entities;
public class Opponent : Entity
{
    protected Opponent() { }

    public Opponent(string stadium, string name)
    {
        Stadium = stadium;
        Name = name;
    }

    public long TeamId { get; private set; }
    public Team Team { get; private set; } = null!;
    public string Name { get; private set; } = string.Empty;
    public string Stadium { get; private set; } = string.Empty;
}
