using CleanSheet.Domain.Entities;

namespace CleanSheet.Application.Abstractions;
public interface IJwtProvider
{
    string Generate(User user);
}
