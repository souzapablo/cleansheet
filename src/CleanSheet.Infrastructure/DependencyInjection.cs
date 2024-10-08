﻿using CleanSheet.Application.Abstractions;
using CleanSheet.Domain.Abstractions;
using CleanSheet.Domain.Repositories;
using CleanSheet.Infrastructure.Authentication;
using CleanSheet.Infrastructure.Repositories;
using CleanSheet.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanSheet.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddRepositories();
        services.AddSecurity();
        return services;
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

        services.AddDbContext<CleanSheetDbContext>(options =>
            options.UseNpgsql(connectionString));
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICareerRepository, CareerRepository>();
        services.AddScoped<IInitialTeamRepository, InitialTeamRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IOpponentRepository, OpponentRepository>();
        services.AddScoped<IMatchRepository, MatchRepository>();
    }

    private static void AddSecurity(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();
    }
}
