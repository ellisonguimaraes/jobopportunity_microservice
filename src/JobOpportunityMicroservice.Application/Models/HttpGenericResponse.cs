using System.Diagnostics;
using System.Text.Json.Serialization;

namespace JobOpportunityMicroservice.Application.Models;

public class GenericHttpResponse<T>
{
    [JsonPropertyName("trace_id")]
    public string? TraceId { get; set; }

    [JsonIgnore]
    public int StatusCode { get; set; }

    [JsonPropertyName("data")]
    public T? Data { get; set; }

    [JsonPropertyName("errors")]
    public IEnumerable<string> Errors { get; set; }

    public GenericHttpResponse()
    {
        TraceId = Activity.Current?.Id;
        Errors = Enumerable.Empty<string>();
    }
}

public class GenericHttpResponse
{
    [JsonPropertyName("trace_id")]
    public string? TraceId { get; set; }

    [JsonIgnore]
    public int StatusCode { get; set; }

    [JsonPropertyName("data")]
    public dynamic? Data { get; set; }

    [JsonPropertyName("errors")]
    public IEnumerable<string> Errors { get; set; }

    public GenericHttpResponse()
    {
        TraceId = Activity.Current?.Id;
        Errors = Enumerable.Empty<string>();
    }
}