using Bogus;
using CleanSheet.Domain.Entities;

namespace CleanSheet.UnitTests.Fakers.Opponents;
public static class OpponentFaker
{
    public static Opponent CreateFaker() =>
        new Faker<Opponent>()
            .CustomInstantiator(f => new Opponent(
                f.Address.StreetName(),  
                f.Name.FirstName()      
            ))
            .RuleFor(o => o.Id, f => f.Random.Long(1))
            .Generate();
}
