using CleanSheet.Domain.Abstractions;

namespace CleanSheet.Domain.Entities;
public class Career : Entity
{
    private readonly List<Team> _teams = [];

    protected Career() { }
    public Career(Manager manager)
    {
        Manager = manager;
    }

    public Manager Manager { get; private set; } = null!;
    public DateTime LastUpdate { get; private set; } = DateTime.UtcNow;
    public long UserId { get; private set; }
    public User User { get; private set; } = null!;
    public IReadOnlyCollection<Team> Teams => _teams;

    public static Result<Career> Create(Manager manager, InitialTeam initialTeam)
    {
        var newCareer = new Career(manager);

        var newTeamResult = Team.CreateInitialTeam(initialTeam);

        if (!newTeamResult.IsSuccess)
            return Result.Failure<Career>(newTeamResult.Error);

        newCareer.AddTeam(newTeamResult.Value);

        return Result.Success(newCareer);
    }

    public void AddTeam(Team team) =>
        _teams.Add(team);
}
