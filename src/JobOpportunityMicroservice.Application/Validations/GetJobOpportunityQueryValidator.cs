using System.Reflection;
using FluentValidation;
using JobOpportunityMicroservice.Application.Queries.GetJobOpportunity;
using JobOpportunityMicroservice.Infra.CrossCutting.Resource;
using Newtonsoft.Json;

namespace JobOpportunityMicroservice.Application.Validations;

public class GetJobOpportunityQueryValidator : AbstractValidator<GetJobOpportunityQuery>
{
    #region Constants
    private const string PROPERTY_NAME = "{PropertyName}";
    #endregion
    
    private readonly Type _type = typeof(GetJobOpportunityQuery);
    
    public GetJobOpportunityQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithName(GetJsonPropertyName(_type, nameof(GetJobOpportunityQuery.Id))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_NULL, PROPERTY_NAME))
            .Must(x => Guid.TryParseExact(x, "D", out _)).WithName(GetJsonPropertyName(_type, nameof(GetJobOpportunityQuery.Id))).WithMessage(string.Format(ErrorCodeResource.INVALID_PROPERTY, PROPERTY_NAME));
    }
    
    /// <summary>
    /// Get JsonPropertyName attribute
    /// </summary>
    /// <param name="type">Type</param>
    /// <param name="attr">Attribute name</param>
    private string GetJsonPropertyName(Type type, string attr)
    {
        var property = type.GetProperty(attr);
        var attribute = property?.GetCustomAttribute<JsonPropertyAttribute>();
        return attribute?.PropertyName ?? attr;
    }
}