using Bogus;
using CleanSheet.Domain.Entities;
using System.Reflection;

namespace CleanSheet.UnitTests.Fakers.Teams;
public static class TeamFaker
{
    public static Team CreateFaker() =>
        new Faker<Team>()
            .CustomInstantiator(f =>
            {
                var ctor = typeof(Team).GetConstructor(
                    BindingFlags.NonPublic | BindingFlags.Instance,
                    null,
                    [typeof(string), typeof(string)],
                    null);

                return (ctor?.Invoke([
                f.Address.City(),        
                f.Address.StreetName()  
                ]) as Team)!;
            })
            .Generate();
}
