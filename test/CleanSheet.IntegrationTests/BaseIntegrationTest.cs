﻿using CleanSheet.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanSheet.IntegrationTests;
public class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly CleanSheetDbContext DbContext;
    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<CleanSheetDbContext>();
    }
}
