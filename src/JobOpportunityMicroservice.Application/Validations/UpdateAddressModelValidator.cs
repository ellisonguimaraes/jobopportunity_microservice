using System.Reflection;
using FluentValidation;
using JobOpportunityMicroservice.Application.Commands.UpdateJobOpportunity;
using JobOpportunityMicroservice.Infra.CrossCutting.Resource;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JobOpportunityMicroservice.Application.Validations;

/// <summary>
/// Address Model Validator
/// </summary>
public class UpdateAddressModelValidator : AbstractValidator<AddressRequest>
{
    #region Constants
    private const string PROPERTY_NAME = "{PropertyName}";
    private const int CITY_MAX_LENGTH = 300;
    private const int COUNTRY_MAX_LENGTH = 300;
    private const int DISTRICT_MAX_LENGTH = 300;
    private const int STATE_MAX_LENGTH = 300;
    private const int STREET_MAX_LENGTH = 300;
    #endregion
    
    private readonly Type _type = typeof(AddressRequest);

    public UpdateAddressModelValidator()
    {
        RuleFor(jo => jo.Id)
            .NotNull().WithName(GetJsonPropertyName(_type, nameof(AddressRequest.Id))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_NULL, PROPERTY_NAME))
            .Must(x => Guid.TryParseExact(x, "D", out _)).WithName(GetJsonPropertyName(_type, nameof(AddressRequest.Id))).WithMessage(string.Format(ErrorCodeResource.INVALID_PROPERTY, PROPERTY_NAME));
        
        RuleFor(a => a.City)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddressRequest.City))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_EMPTY, PROPERTY_NAME))
            .MaximumLength(CITY_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(AddressRequest.City))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, CITY_MAX_LENGTH));
        
        RuleFor(a => a.Country)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddressRequest.Country))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_EMPTY, PROPERTY_NAME))
            .MaximumLength(COUNTRY_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(AddressRequest.Country))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, COUNTRY_MAX_LENGTH));
        
        RuleFor(a => a.District)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddressRequest.District))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_EMPTY, PROPERTY_NAME))
            .MaximumLength(DISTRICT_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(AddressRequest.District))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, DISTRICT_MAX_LENGTH));
        
        RuleFor(a => a.State)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddressRequest.State))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_EMPTY, PROPERTY_NAME))
            .MaximumLength(STATE_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(AddressRequest.State))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, STATE_MAX_LENGTH));
        
        RuleFor(a => a.Street)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddressRequest.Street))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_EMPTY, PROPERTY_NAME))
            .MaximumLength(STREET_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(AddressRequest.Street))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, STREET_MAX_LENGTH));
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