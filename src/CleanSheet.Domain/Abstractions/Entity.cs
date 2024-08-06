namespace CleanSheet.Domain.Abstractions;
public abstract class Entity
{
    public long Id { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
}
