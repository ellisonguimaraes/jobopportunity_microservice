using MediatR;
using Newtonsoft.Json;

namespace JobOpportunityMicroservice.Application.Commands.DeleteJobOpportunity;

public record DeleteJobOpportunityCommand : IRequest<Guid>
{
    [JsonProperty("id")] 
    public string Id { get; set; }
}