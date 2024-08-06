using CleanSheet.Domain.Abstractions;
using CleanSheet.Domain.Enums;

namespace CleanSheet.Domain.Entities;
public class Player : Entity
{
    protected Player() { }
    public Player(string name, int kitNumber, int overall, DateOnly birthday, PlayerPosition position)
    {
        Name = name;
        KitNumber = kitNumber;
        Overall = overall;
        Birthday = birthday;
        Position = position;
    }

    public string Name { get; private set; } = string.Empty;
    public int KitNumber { get; private set; }
    public int Overall { get; private set; }
    public DateOnly Birthday { get; private set; }
    public PlayerPosition Position { get; private set; }
}
