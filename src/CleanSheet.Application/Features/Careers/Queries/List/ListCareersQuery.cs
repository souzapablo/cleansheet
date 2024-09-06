using CleanSheet.Application.Features.Careers.Responses;
using CleanSheet.Domain.Abstractions;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Queries.List;
public record ListCareersQuery(
    long UserId) : IRequest<Result<List<CareerResponse>>>;
