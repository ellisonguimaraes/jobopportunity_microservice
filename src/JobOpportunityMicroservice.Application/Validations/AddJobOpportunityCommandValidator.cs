using System.Globalization;
using System.Net.Mail;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using FluentValidation;
using JobOpportunityMicroservice.Application.Commands.AddJobOpportunity;
using JobOpportunityMicroservice.Domain.Enums;

namespace JobOpportunityMicroservice.Application.Validations;

/// <summary>
/// Add Job Opportunity Command Validator
/// </summary>
public class AddJobOpportunityCommandValidator : AbstractValidator<AddJobOpportunityCommand>
{
    #region Constants
    private const string PROPERTY_IS_EMPTY = "{PropertyName} is empty";
    private const string PROPERTY_IS_NULL = "{PropertyName} is null";
    private const string PROPERTY_MORE_THAN_CHARACTERS = "{0} more than {1} characters";
    private const string PROPERTY_NAME = "{PropertyName}";
    private const string PROPERTY_INVALID = "Invalid {PropertyName}";
    private const string DATETIME_FORMAT = "dd/MM/yyyy";
    private const int TITLE_MAX_LENGTH = 150;
    private const int COMPANY_MAX_LENGTH = 80;
    private const int EMAIL_MAX_LENGTH = 100;
    private const int PHONE_NUMBER_MAX_LENGTH = 45;
    private const int LINK_MAX_LENGTH = 300;
    private const string PHONE_NUMBER_REGEX_PATTERN = @"^\d+$";
    #endregion
    
    private readonly Type _type = typeof(AddJobOpportunityCommand);

    public AddJobOpportunityCommandValidator()
    {
        RuleFor(jo => jo.Title)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Title))).WithMessage(PROPERTY_IS_EMPTY)
            .MaximumLength(TITLE_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Title))).WithMessage(string.Format(PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, TITLE_MAX_LENGTH));
        
        RuleFor(jo => jo.Company)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Company))).WithMessage(PROPERTY_IS_EMPTY)
            .MaximumLength(COMPANY_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Company))).WithMessage(string.Format(PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, COMPANY_MAX_LENGTH));

        RuleFor(jo => jo.Description)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Description))).WithMessage(PROPERTY_IS_EMPTY);

        RuleFor(jo => jo.Modality)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Modality))).WithMessage(PROPERTY_IS_EMPTY)
            .IsEnumName(typeof(Modality), caseSensitive: false).WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Modality))).WithMessage(PROPERTY_INVALID);

        RuleFor(jo => jo.Benefit)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Benefit))).WithMessage(PROPERTY_IS_EMPTY);

        RuleFor(jo => jo.MinPayRange)
            .NotNull().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.MinPayRange))).WithMessage(PROPERTY_IS_NULL);

        RuleFor(jo => jo.MaxPayRange)
            .NotNull().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.MaxPayRange))).WithMessage(PROPERTY_IS_NULL);

        RuleFor(jo => jo.Requerements)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Requerements))).WithMessage(PROPERTY_IS_EMPTY);

        RuleFor(jo => jo.MonthlyHours)
            .NotNull().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.MonthlyHours))).WithMessage(PROPERTY_IS_NULL);

        RuleFor(jo => jo.Email)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Email))).WithMessage(PROPERTY_IS_EMPTY)
            .MaximumLength(EMAIL_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Email))).WithMessage(string.Format(PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, EMAIL_MAX_LENGTH))
            .Must(e => MailAddress.TryCreate(e, out _)).WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Email))).WithMessage(PROPERTY_INVALID);

        RuleFor(jo => jo.PhoneNumber)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.PhoneNumber))).WithMessage(PROPERTY_IS_EMPTY)
            .MaximumLength(PHONE_NUMBER_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.PhoneNumber))).WithMessage(string.Format(PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, PHONE_NUMBER_MAX_LENGTH))
            .Must(pn => Regex.IsMatch(pn ?? string.Empty, PHONE_NUMBER_REGEX_PATTERN)).WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.PhoneNumber))).WithMessage(PROPERTY_INVALID);

        RuleFor(jo => jo.Link)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Link))).WithMessage(PROPERTY_IS_EMPTY)
            .MaximumLength(LINK_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Link))).WithMessage(string.Format(PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, LINK_MAX_LENGTH))
            .Must(l => Uri.TryCreate(l, UriKind.Absolute, out _)).WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Link))).WithMessage(PROPERTY_INVALID);

        RuleFor(jo => jo.DateLimit)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.DateLimit))).WithMessage(PROPERTY_IS_EMPTY)
            .Must(d => DateTime.TryParseExact(d, DATETIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out _)).WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.DateLimit))).WithMessage(PROPERTY_INVALID);

        RuleFor(jo => jo.Address)
            .SetValidator(new AddAddressModelValidator());
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