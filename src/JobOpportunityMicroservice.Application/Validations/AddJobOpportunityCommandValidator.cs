using System.Globalization;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;
using FluentValidation;
using JobOpportunityMicroservice.Application.Commands.AddJobOpportunity;
using JobOpportunityMicroservice.Domain.Enums;
using JobOpportunityMicroservice.Infra.CrossCutting.Resource;
using Newtonsoft.Json;

namespace JobOpportunityMicroservice.Application.Validations;

/// <summary>
/// Add Job Opportunity Command Validator
/// </summary>
public class AddJobOpportunityCommandValidator : AbstractValidator<AddJobOpportunityCommand>
{
    #region Constants
    private const string PROPERTY_NAME = "{PropertyName}";
    private const string DATETIME_FORMAT = "dd/MM/yyyy HH:mm:ss";
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
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Title))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_EMPTY, PROPERTY_NAME))
            .MaximumLength(TITLE_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Title))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, TITLE_MAX_LENGTH));
        
        RuleFor(jo => jo.Company)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Company))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_EMPTY, PROPERTY_NAME))
            .MaximumLength(COMPANY_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Company))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, COMPANY_MAX_LENGTH));

        RuleFor(jo => jo.Description)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Description))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_EMPTY, PROPERTY_NAME));

        RuleFor(jo => jo.Modality)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Modality))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_EMPTY, PROPERTY_NAME))
            .IsEnumName(typeof(Modality), caseSensitive: false).WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Modality))).WithMessage(string.Format(ErrorCodeResource.INVALID_PROPERTY, PROPERTY_NAME));

        RuleFor(jo => jo.Benefit)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Benefit))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_EMPTY, PROPERTY_NAME));

        RuleFor(jo => jo.MinPayRange)
            .NotNull().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.MinPayRange))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_NULL, PROPERTY_NAME));

        RuleFor(jo => jo.MaxPayRange)
            .NotNull().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.MaxPayRange))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_NULL, PROPERTY_NAME));

        RuleFor(jo => jo.Requerements)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Requerements))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_EMPTY, PROPERTY_NAME));

        RuleFor(jo => jo.MonthlyHours)
            .NotNull().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.MonthlyHours))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_NULL, PROPERTY_NAME));

        RuleFor(jo => jo.Email)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Email))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_EMPTY, PROPERTY_NAME))
            .MaximumLength(EMAIL_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Email))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, EMAIL_MAX_LENGTH))
            .Must(e => MailAddress.TryCreate(e, out _)).WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Email))).WithMessage(string.Format(ErrorCodeResource.INVALID_PROPERTY, PROPERTY_NAME));

        RuleFor(jo => jo.PhoneNumber)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.PhoneNumber))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_EMPTY, PROPERTY_NAME))
            .MaximumLength(PHONE_NUMBER_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.PhoneNumber))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, PHONE_NUMBER_MAX_LENGTH))
            .Must(pn => Regex.IsMatch(pn ?? string.Empty, PHONE_NUMBER_REGEX_PATTERN)).WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.PhoneNumber))).WithMessage(string.Format(ErrorCodeResource.INVALID_PROPERTY, PROPERTY_NAME));

        RuleFor(jo => jo.Link)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Link))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_EMPTY, PROPERTY_NAME))
            .MaximumLength(LINK_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Link))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, LINK_MAX_LENGTH))
            .Must(l => Uri.TryCreate(l, UriKind.Absolute, out _)).WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Link))).WithMessage(string.Format(ErrorCodeResource.INVALID_PROPERTY, PROPERTY_NAME));

        RuleFor(jo => jo.DateLimit)
            .NotEmpty().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.DateLimit))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_EMPTY, PROPERTY_NAME))
            .Must(d => DateTime.TryParseExact(d, DATETIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out _)).WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.DateLimit))).WithMessage(string.Format(ErrorCodeResource.INVALID_PROPERTY, PROPERTY_NAME));
    
        RuleFor(jo => jo.Categories)
            .NotNull().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Categories))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_NULL, PROPERTY_NAME));
        
        RuleFor(jo => jo.Address)
            .NotNull().WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.Address))).WithMessage(string.Format(ErrorCodeResource.PROPERTY_IS_NULL, PROPERTY_NAME))
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
        var attribute = property?.GetCustomAttribute<JsonPropertyAttribute>();
        return attribute?.PropertyName ?? attr;
    }
}