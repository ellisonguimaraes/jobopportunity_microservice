using System.Diagnostics;
using Newtonsoft.Json;

namespace JobOpportunityMicroservice.Application.Models;

public class GenericHttpResponse<T>
{
    [JsonProperty("trace_id")]
    public string? TraceId { get; set; }

    [JsonIgnore]
    public int StatusCode { get; set; }

    [JsonProperty("data")]
    public T? Data { get; set; }

    [JsonProperty("errors")]
    public IEnumerable<string> Errors { get; set; }

    public GenericHttpResponse()
    {
        TraceId = Activity.Current?.Id;
        Errors = Enumerable.Empty<string>();
    }
}

public class GenericHttpResponse
{
    [JsonProperty("trace_id")]
    public string? TraceId { get; set; }

    [JsonIgnore]
    public int StatusCode { get; set; }

    [JsonProperty("data")]
    public dynamic? Data { get; set; }

    [JsonProperty("errors")]
    public IEnumerable<string> Errors { get; set; }

    public GenericHttpResponse()
    {
        TraceId = Activity.Current?.Id;
        Errors = Enumerable.Empty<string>();
    }
}