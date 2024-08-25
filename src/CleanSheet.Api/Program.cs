using CleanSheet.Api.Infrastructure;
using CleanSheet.Api.OptionsSetup;
using CleanSheet.Application;
using CleanSheet.Infrastructure;
using CleanSheet.Infrastructure.Authentication;
using CleanSheet.Presentation.Endpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
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

    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header with Bearer scheme."
    });

    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
            Array.Empty<string>()
        }
    });
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services
    .AddAuthorization();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services
    .ConfigureOptions<JwtOptionsSetup>();

builder.Services
    .ConfigureOptions<JwtBearerOptionsSetup>();

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

app.UseAuthentication();

app.UseAuthorization();

app.UseExceptionHandler();

app.MapEndpoints();
app.Run();

public partial class Program { }