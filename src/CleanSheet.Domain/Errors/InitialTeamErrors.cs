using CleanSheet.Domain.Abstractions;

namespace CleanSheet.Domain.Errors;
public class InitialTeamErrors
{
    public static Error NotFound = new("INITAL_TEAM_NOT_FOUND", "Initial team not found.");
    public static Error EmptyInitialSquad = new("EMPTY_INITIAL_SQUAD", "Initial squad is empty.");
}
