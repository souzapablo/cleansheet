using CleanSheet.Domain.Abstractions;

namespace CleanSheet.Domain.Entities;
public class Assist : Entity
{
    protected Assist() { }

    public Goal Goal { get; private set; } = null!;
    public long PlayerAssistedId { get; set; }
    public Player PlayerAssisted { get; private set; } = null!;
}
