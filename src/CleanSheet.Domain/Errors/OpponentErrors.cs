using CleanSheet.Domain.Abstractions;

namespace CleanSheet.Domain.Errors;
public class OpponentErrors
{
    public static Error NotFound = new("OPPONENT_NOT_FOUND", "Opponent not found.");
}
