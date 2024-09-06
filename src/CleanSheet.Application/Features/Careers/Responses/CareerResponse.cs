namespace CleanSheet.Application.Features.Careers.Responses;
public record CareerResponse(
    string Manager,
    string? CurrentTeam,
    DateTime LastUpdate);