using System.Diagnostics;

namespace JobOpportunityMicroservice.Domain.Exceptions;

/// <summary>
/// Generic business exception
/// </summary>
public class BusinessException : Exception
{
    public string? TraceId { get; set; }

    public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();

    public BusinessException(string? message) : base(message)
    {
        TraceId = Activity.Current?.Id;
    }
    
    public BusinessException(string message, IEnumerable<string> errors) : base(message)
    {
        TraceId = Activity.Current?.Id;
        Errors = errors;
    }

    public BusinessException(string? message, Exception? innerException) : base(message, innerException)
    {
        TraceId = Activity.Current?.Id;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, TraceId: {TraceId}";
    }
}