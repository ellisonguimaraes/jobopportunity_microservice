using FluentValidation;
using JobOpportunityMicroservice.Application.Behaviors;
using JobOpportunityMicroservice.Application.Commands.AddJobOpportunity;
using JobOpportunityMicroservice.Application.Commands.UpdateJobOpportunity;
using JobOpportunityMicroservice.Application.Mapper;
using JobOpportunityMicroservice.Application.Models;
using JobOpportunityMicroservice.Application.Services;
using JobOpportunityMicroservice.Application.Services.Interfaces;
using JobOpportunityMicroservice.Application.Validations;
using JobOpportunityMicroservice.Domain;
using JobOpportunityMicroservice.Infra.Data.Models;
using JobOpportunityMicroservice.Infra.Data.Repositories;
using JobOpportunityMicroservice.Infra.Data.Repositories.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Address = JobOpportunityMicroservice.Domain.Address;

namespace JobOpportunityMicroservice.Infra.CrossCutting.IoC;

/// <summary>
/// IServiceCollection extension to register services
/// </summary>
public static class DependencyInjectionExtensions
{ 
    #region Constants
    private const bool ASSUME_DEFAULT_VERSION_WHEN_UNSPECIFIED = true;
    private const string API_DEFAULT_VERSION_PROPERTY = "ApiDefaultVersion";
    private const bool REPORT_API_VERSIONS = true;
    private const string HEADER_API_VERSION = "X-Version";
    private const string QUERY_STRING_API_VERSION = "api-version";
    private const string MEDIA_TYPE_API_VERSION = "ver";
    private const string FORMAT_API_VERSION = "'v'VVV";
    private const bool SUBSTITUTE_API_VERSION_IN_URL = true;
    private const string DOT = ".";
    #endregion

    /// <summary>
    /// Register services
    /// </summary>
    /// <param name="services">Service Collection</param>
    /// <param name="configuration">Configuration file (appsettings)</param>
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Repositories
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IRepository<Address>, AddressRepository>();
        services.AddScoped<IRepository<JobAdvertisement>, JobAdvertisementRepository>();
        services.AddScoped<IRepository<JobCategory>, Repository<JobCategory>>();
        
        // Mapper
        services.AddAutoMapper(typeof(JobAdvertisementProfile));
        
        // Validators
        services.AddTransient<IValidator<PaginationParametersRequest>, PaginationParametersRequestValidator>();
        services.AddTransient<IValidator<AddJobOpportunityCommand>, AddJobOpportunityCommandValidator>();
        services.AddTransient<IValidator<UpdateJobOpportunityCommand>, UpdateJobOpportunityCommandValidator>();
        
        // Services
        services.AddScoped<IJobAdvertisementServices, JobAdvertisementServices>();
        
        // Behaviors
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }

    /// <summary>
    /// Register versioning services
    /// </summary>
    /// <param name="services">Service Collection</param>
    /// <param name="configuration">Configuration file (appsettings)</param>
    public static void AddApiVersioningConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var defaultVersion = configuration[API_DEFAULT_VERSION_PROPERTY].Split(DOT);
        var majorVersion = int.Parse(defaultVersion.First());
        var minorVersion = int.Parse(defaultVersion.Last());

        services.AddApiVersioning(options => {
            options.AssumeDefaultVersionWhenUnspecified = ASSUME_DEFAULT_VERSION_WHEN_UNSPECIFIED;
            options.DefaultApiVersion = new ApiVersion(majorVersion, minorVersion);
            options.ReportApiVersions = REPORT_API_VERSIONS;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader(QUERY_STRING_API_VERSION),
                new HeaderApiVersionReader(HEADER_API_VERSION),
                new MediaTypeApiVersionReader(MEDIA_TYPE_API_VERSION)
            );
        });
        
        services.AddVersionedApiExplorer(setup => {
            setup.GroupNameFormat = FORMAT_API_VERSION;
            setup.SubstituteApiVersionInUrl = SUBSTITUTE_API_VERSION_IN_URL;
        });
    }
}
