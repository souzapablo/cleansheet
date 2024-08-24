﻿using CleanSheet.Domain.Abstractions;

namespace CleanSheet.Domain.Entities;
public class Career : Entity
{
    private readonly List<Team> _teams = [];

    protected Career() { }
    public Career(Manager manager)
    {
        Manager = manager;
    }

    public Manager Manager { get; private set; }
    public DateTime LastUpdate { get; private set; } = DateTime.UtcNow;
    public long UserId { get; private set; }
    public User User { get; private set; } = null!;
    public IReadOnlyCollection<Team> Teams => _teams;
}
