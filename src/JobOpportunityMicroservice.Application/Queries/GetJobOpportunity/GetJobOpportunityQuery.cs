using JobOpportunityMicroservice.Application.Commands.JobOpportunity;
using MediatR;
using Newtonsoft.Json;

namespace JobOpportunityMicroservice.Application.Queries.GetJobOpportunity;

public record GetJobOpportunityQuery : IRequest<JobOpportunityCommandResponse>
{
    [JsonProperty("id")]
    public string Id { get; set; }
}