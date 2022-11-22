using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation;
using JobOpportunityMicroservice.Application.Commands.AddJobOpportunity;

namespace JobOpportunityMicroservice.Application.Validations;

/// <summary>
/// Address Model Validator
/// </summary>
public class AddAddressModelValidator : AbstractValidator<AddressRequest>
{
    private const string PROPERTY_IS_EMPTY = "{PropertyName} is empty";
    private const string PROPERTY_MORE_THAN_CHARACTERS = "{0} more than {1} characters";
    private const string PROPERTY_NAME = "{PropertyName}";
    private const int CITY_MAX_LENGTH = 300;
    private const int COUNTRY_MAX_LENGTH = 300;
    private const int DISTRICT_MAX_LENGTH = 300;
    private const int STATE_MAX_LENGTH = 300;
    private const int STREET_MAX_LENGTH = 300;
    
    private readonly Type _type = typeof(AddressRequest);

    public AddAddressModelValidator()
    {
        RuleFor(a => a.City)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddressRequest.City))).WithMessage(PROPERTY_IS_EMPTY)
            .MaximumLength(CITY_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(AddressRequest.City))).WithMessage(string.Format(PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, CITY_MAX_LENGTH));
        
        RuleFor(a => a.Country)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddressRequest.Country))).WithMessage(PROPERTY_IS_EMPTY)
            .MaximumLength(COUNTRY_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(AddressRequest.Country))).WithMessage(string.Format(PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, COUNTRY_MAX_LENGTH));
        
        RuleFor(a => a.District)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddressRequest.District))).WithMessage(PROPERTY_IS_EMPTY)
            .MaximumLength(DISTRICT_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(AddressRequest.District))).WithMessage(string.Format(PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, DISTRICT_MAX_LENGTH));
        
        RuleFor(a => a.State)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddressRequest.State))).WithMessage(PROPERTY_IS_EMPTY)
            .MaximumLength(STATE_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(AddressRequest.State))).WithMessage(string.Format(PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, STATE_MAX_LENGTH));
        
        RuleFor(a => a.Street)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddressRequest.Street))).WithMessage(PROPERTY_IS_EMPTY)
            .MaximumLength(STREET_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(AddressRequest.Street))).WithMessage(string.Format(PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, STREET_MAX_LENGTH));
    }
    
    /// <summary>
    /// Get JsonPropertyName attribute
    /// </summary>
    /// <param name="type">Type</param>
    /// <param name="attr">Attribute name</param>
    private string GetJsonPropertyName(Type type, string attr)
    {
        var property = type.GetProperty(attr);
        var attribute = property?.GetCustomAttribute<JsonPropertyNameAttribute>();
        return attribute?.Name ?? attr;
    }
}