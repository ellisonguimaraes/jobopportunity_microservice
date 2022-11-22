using JobOpportunityMicroservice.API.Middleware;
using JobOpportunityMicroservice.Application.Commands.AddJobOpportunity;
using JobOpportunityMicroservice.Application.Commands.UpdateJobOpportunity;
using JobOpportunityMicroservice.Infra.CrossCutting.IoC;
using JobOpportunityMicroservice.Infra.Data.Contexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

#region Constants
const string APPLICATION_PROPERTY = "Application";
const string APPLICATION_NAME = "ApplicationName";
const string JOB_DB_CONNECTION_STRING_PROPERTY = "JobDbConnectionString";
const string HEALTH_CHECK_ROUTE = "/health";
#endregion

var builder = WebApplication.CreateBuilder(args);

#region Configure Services
// Serilog Configuration
builder.Logging.ClearProviders();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.WithProperty(APPLICATION_PROPERTY, builder.Configuration[APPLICATION_NAME])
    .CreateLogger();

builder.Logging.AddSerilog(logger);

// Database Context Configuration
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration.GetConnectionString(JOB_DB_CONNECTION_STRING_PROPERTY));

// Versioning API Configuration
builder.Services.AddApiVersioningConfiguration(builder.Configuration);

// Healt Check Configuration
builder.Services.AddHealthChecks();

// Clear .NET Built-in Validator (in the action inside the controller)
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Add MediatR
builder.Services.AddMediatR(typeof(AddJobOpportunityCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(UpdateJobOpportunityCommandHandler).Assembly);

// Add services to the container.
builder.Services.AddControllers();

// Register Services
builder.Services.RegisterServices(builder.Configuration);
#endregion

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHealthChecks(HEALTH_CHECK_ROUTE);

app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();
