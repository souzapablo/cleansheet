namespace CleanSheet.Domain.Abstractions;
public sealed record Error(
    string Code,
    string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public static readonly Error NullValue = new("NULL_VALUE", "The specified result value is null.");

    public static readonly Error ConditionNotMet = new("CONDITION_NOT_MET", "The specified condition was not met.");
};