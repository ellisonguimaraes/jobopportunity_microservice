using System.Reflection;
using FluentValidation;
using JobOpportunityMicroservice.Application.Queries.GetAllJobOpportunity;
using JobOpportunityMicroservice.Infra.CrossCutting.Resource;
using Microsoft.AspNetCore.Mvc;

namespace JobOpportunityMicroservice.Application.Validations;

public class GetAllJobOpportunityQueryValidator : AbstractValidator<GetAllJobOpportunityQuery>
{
    #region Constants
    private const string PROPERTY_NAME = "{PropertyName}";
    #endregion
    
    private readonly Type _type = typeof(GetAllJobOpportunityQuery);
    
    public GetAllJobOpportunityQueryValidator()
    {
        RuleFor(pp => pp.PageNumber)
            .NotNull().WithName(GetJsonPropertyName(_type, nameof(GetAllJobOpportunityQuery.PageNumber))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_NULL, PROPERTY_NAME));
        
        RuleFor(pp => pp.PageSize)
            .NotNull().WithName(GetJsonPropertyName(_type, nameof(GetAllJobOpportunityQuery.PageSize))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_NULL, PROPERTY_NAME));
    }
    
    /// <summary>
    /// Get JsonPropertyName attribute
    /// </summary>
    /// <param name="type">Type</param>
    /// <param name="attr">Attribute name</param>
    private string GetJsonPropertyName(Type type, string attr)
    {
        var property = type.GetProperty(attr);
        var attribute = property?.GetCustomAttribute<BindPropertyAttribute>();
        return attribute?.Name ?? attr;
    }
}