using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation;
using JobOpportunityMicroservice.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobOpportunityMicroservice.Application.Validations;

/// <summary>
/// Pagination Parameters Request Model Validator
/// </summary>
public class PaginationParametersRequestValidator : AbstractValidator<PaginationParametersRequest>
{
    #region Constants
    private const string PROPERTY_NOT_NULL = "{PropertyName} is null";
    #endregion
    
    private readonly Type _type = typeof(PaginationParametersRequest);
    
    public PaginationParametersRequestValidator()
    {
        RuleFor(pp => pp.PageNumber)
            .NotNull().WithName(GetJsonPropertyName(_type, nameof(PaginationParametersRequest.PageNumber))).WithMessage(PROPERTY_NOT_NULL);
        
        RuleFor(pp => pp.PageSize)
            .NotNull().WithName(GetJsonPropertyName(_type, nameof(PaginationParametersRequest.PageSize))).WithMessage(PROPERTY_NOT_NULL);
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