using CleanSheet.Domain.Abstractions;

namespace CleanSheet.Domain.Errors;
public class TeamErrors
{
    public static Error NotFound = new("TEAM_NOT_FOUND", "Team not found.");
}
