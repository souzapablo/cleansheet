namespace CleanSheet.Domain.Abstractions;
public sealed record Error(
    string Code,
    string Message);