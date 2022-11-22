using System.Globalization;
using System.Net.Mail;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using FluentValidation;
using JobOpportunityMicroservice.Application.Commands.AddJobOpportunity;
using JobOpportunityMicroservice.Application.Commands.UpdateJobOpportunity;
using JobOpportunityMicroservice.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace JobOpportunityMicroservice.Application.Validations;

public class UpdateJobOpportunityCommandValidator : AbstractValidator<UpdateJobOpportunityCommand>
{
    #region Constants
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
    
    private readonly Type _type = typeof(UpdateJobOpportunityCommand);
    
    public UpdateJobOpportunityCommandValidator()
    {
        RuleFor(jo => jo.Id)
            .NotNull().WithName(GetJsonPropertyName(_type, nameof(UpdateJobOpportunityCommand.Id))).WithMessage(PROPERTY_IS_NULL)
            .Must(x => Guid.TryParseExact(x, "D", out _)).WithName(GetJsonPropertyName(_type, nameof(UpdateJobOpportunityCommand.Id))).WithMessage(PROPERTY_INVALID);
        
        RuleFor(jo => jo.Title)
            .MaximumLength(TITLE_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(UpdateJobOpportunityCommand.Title))).WithMessage(string.Format(PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, TITLE_MAX_LENGTH));
        
        RuleFor(jo => jo.Company)
            .MaximumLength(COMPANY_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(UpdateJobOpportunityCommand.Company))).WithMessage(string.Format(PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, COMPANY_MAX_LENGTH));

        RuleFor(jo => jo.Modality)
            .IsEnumName(typeof(Modality), caseSensitive: false).WithName(GetJsonPropertyName(_type, nameof(UpdateJobOpportunityCommand.Modality))).WithMessage(PROPERTY_INVALID);

        RuleFor(jo => jo.Email)
            .MaximumLength(EMAIL_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(UpdateJobOpportunityCommand.Email))).WithMessage(string.Format(PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, EMAIL_MAX_LENGTH))
            .Must(e => MailAddress.TryCreate(e, out _)).WithName(GetJsonPropertyName(_type, nameof(UpdateJobOpportunityCommand.Email))).WithMessage(PROPERTY_INVALID)
                .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(jo => jo.PhoneNumber)
            .MaximumLength(PHONE_NUMBER_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(UpdateJobOpportunityCommand.PhoneNumber))).WithMessage(string.Format(PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, PHONE_NUMBER_MAX_LENGTH))
            .Must(pn => Regex.IsMatch(pn ?? string.Empty, PHONE_NUMBER_REGEX_PATTERN)).WithName(GetJsonPropertyName(_type, nameof(UpdateJobOpportunityCommand.PhoneNumber))).WithMessage(PROPERTY_INVALID)
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        
        RuleFor(jo => jo.Link)
            .MaximumLength(LINK_MAX_LENGTH).WithName(GetJsonPropertyName(_type, nameof(UpdateJobOpportunityCommand.Link))).WithMessage(string.Format(PROPERTY_MORE_THAN_CHARACTERS, PROPERTY_NAME, LINK_MAX_LENGTH))
            .Must(l => Uri.TryCreate(l, UriKind.Absolute, out _)).WithName(GetJsonPropertyName(_type, nameof(UpdateJobOpportunityCommand.Link))).WithMessage(PROPERTY_INVALID)
                .When(x => !string.IsNullOrEmpty(x.Link));
        
        RuleFor(jo => jo.DateLimit)
            .Must(d => DateTime.TryParseExact(d, DATETIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out _)).WithName(GetJsonPropertyName(_type, nameof(AddJobOpportunityCommand.DateLimit))).WithMessage(PROPERTY_INVALID)
                .When(x => !string.IsNullOrEmpty(x.DateLimit));
        
        RuleFor(jo => jo.Address)
            .SetValidator(new UpdateAddressModelValidator());
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