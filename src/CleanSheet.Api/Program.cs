using CleanSheet.Api.Infrastructure;
using CleanSheet.Application;
using CleanSheet.Infrastructure;
using CleanSheet.Presentation.Endpoints;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new()
    {
        Title = "Clean Sheet API",
        Version = "v1",
        Description = "A simple API for managing football simulator career",
        Contact = new OpenApiContact
        {
            Name = "Pablo Souza",
            Email = "pablo.osouza@outlook.com",
            Url = new Uri("https://github.com/souzapablo/")
        }
    });
});

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Host
    .UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.MapEndpoints();
app.Run();

public partial class Program { }