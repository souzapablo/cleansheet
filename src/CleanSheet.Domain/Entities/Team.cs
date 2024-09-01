﻿using CleanSheet.Domain.Abstractions;
using CleanSheet.Domain.Errors;

namespace CleanSheet.Domain.Entities;
public class Team : Entity
{
    private readonly List<Player> _squad = []; 

    protected Team() { }
    private Team(string name,string stadium)
    {
        Name = name;
        Stadium = stadium;
    }

    public string Name { get; private set; } = string.Empty;
    public string Stadium { get; private set; } = string.Empty;
    public IReadOnlyCollection<Player> Squad => _squad;
    public long CareerId { get; private set; }
    public Career Career { get; private set; } = null!;

    public static Result<Team> CreateInitialTeam(InitialTeam initialTeam)
    {
        var team = new Team(initialTeam.Name, initialTeam.Stadium);

        if (initialTeam.InitialSquad.Count == 0)
            return Result.Failure<Team>(InitialTeamErrors.EmptyInitialSquad);

        var initialPlayers = initialTeam.InitialSquad
            .Select(p => new Player(
                p.Name, 
                p.KitNumber, 
                p.Overall, 
                p.Birthday, 
                p.Position));

        team._squad.AddRange(initialPlayers);

        return team;
    }
}
