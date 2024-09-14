using CleanSheet.Domain.Abstractions;

namespace CleanSheet.Domain.Errors;
public class MatchErrors
{
    public static Error UndefinedNeutralStadium = new("UNDEFINED_NEUTRAL_STADIUM", "Neutral stadium name must be informed.");
}
